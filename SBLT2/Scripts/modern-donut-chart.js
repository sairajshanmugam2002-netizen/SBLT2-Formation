/**
 * Modern Enterprise Donut Chart Component
 * High-precision SVG donut renderer with interactive details table
 * Matching exact reference design and color palette
 */
(function (window, $) {
    'use strict';

    var passedPalette = [
        '#10B981', // 1. Emerald Green
        '#06B6D4', // 2. Cyan / Teal
        '#FBBF24', // 3. Golden Yellow
        '#3B82F6', // 4. Royal Blue
        '#8B5CF6', // 5. Purple / Violet
        '#EC4899', // 6. Pink
        '#14B8A6', // 7. Dark Teal
        '#6366F1'  // 8. Indigo
    ];

    var failedPalette = [
        '#EF4444', // 1. Coral Red
        '#F97316', // 2. Warm Orange
        '#FBBF24', // 3. Golden Yellow
        '#3B82F6', // 4. Royal Blue
        '#8B5CF6', // 5. Purple / Violet
        '#EC4899', // 6. Pink
        '#DC2626', // 7. Crimson
        '#D97706'  // 8. Dark Amber
    ];

    /**
     * Sanitizes and parses records into [{ type: string, qty: number }]
     */
    function sanitizeRecords(rawList) {
        var items = [];
        var total = 0;
        if (!Array.isArray(rawList)) return { items: items, total: total };

        for (var i = 0; i < rawList.length; i++) {
            var r = rawList[i];
            if (!r) continue;

            var type = (r.label || r.mv_BatteryType || r.BatteryType || r.type || ('Type ' + (i + 1))).toString().trim();
            var qty = parseInt(r.y != null ? r.y : (r.Qty != null ? r.Qty : (r.qty != null ? r.qty : (r.TotalPassed != null ? r.TotalPassed : (r.TotalFailed != null ? r.TotalFailed : 0)))), 10);
            if (isNaN(qty) || qty < 0) qty = 0;

            if (qty > 0) {
                items.push({ type: type, qty: qty });
                total += qty;
            }
        }
        return { items: items, total: total };
    }

    /**
     * Renders modern donut chart into DOM container
     * @param {string|jQuery} containerSelector
     * @param {string|Array} dataOrUrl - API endpoint URL string or pre-computed array of items
     * @param {'passed'|'failed'} chartType
     */
    function renderModernDonutChart(containerSelector, dataOrUrl, chartType) {
        var $container = $(containerSelector);
        if (!$container.length) return;

        var isFailed = (chartType === 'failed');
        var palette = isFailed ? failedPalette : passedPalette;

        function drawWithData(items, total) {
            // Empty / Zero state
            if (!items || items.length === 0 || total === 0) {
                $container.html(
                    '<div class="donut-empty-container">' +
                        '<div class="donut-empty-icon"><i class="fa fa-pie-chart"></i></div>' +
                        '<div class="donut-empty-text">No data available</div>' +
                        '<div class="donut-empty-sub">No ' + (isFailed ? 'failed' : 'passed') + ' batteries recorded for selected period</div>' +
                    '</div>'
                );
                return;
            }

            // Geometry constants
            var cx = 120, cy = 120, r = 78, strokeW = 32;
            var C = 2 * Math.PI * r; // ~490.09
            var gap = items.length > 1 ? 3.5 : 0;

            var svgPaths = '<circle cx="' + cx + '" cy="' + cy + '" r="' + r + '" fill="none" ' +
                'stroke="var(--border-color, #e2e8f0)" stroke-width="' + strokeW + '" opacity="0.18" class="donut-track" />';
            var percentLabelsSvg = '';
            var tableRowsHtml = '';
            var accumulatedOffset = 0;

            for (var k = 0; k < items.length; k++) {
                var itm = items[k];
                var pctVal = (itm.qty / total);
                var pctStr = (pctVal * 100).toFixed(1) + '%';
                var segColor = palette[k % palette.length];
                var safeType = $('<div>').text(itm.type).html();

                if (items.length === 1) {
                    svgPaths += '<circle cx="' + cx + '" cy="' + cy + '" r="' + r + '" fill="none" ' +
                        'stroke="' + segColor + '" stroke-width="' + strokeW + '" ' +
                        'class="donut-segment" data-index="' + k + '" />';
                    percentLabelsSvg += '<text x="' + (cx + r) + '" y="' + (cy + 4) + '" ' +
                        'class="donut-segment-pct" text-anchor="middle" data-index="0">100.0%</text>';
                } else {
                    var segLength = pctVal * C;
                    var visibleLen = Math.max(1, segLength - gap);
                    var remainingLen = C - visibleLen;
                    var dashArr = visibleLen.toFixed(2) + ' ' + remainingLen.toFixed(2);
                    var dashOff = (-accumulatedOffset).toFixed(2);

                    svgPaths += '<circle cx="' + cx + '" cy="' + cy + '" r="' + r + '" fill="none" ' +
                        'stroke="' + segColor + '" stroke-width="' + strokeW + '" ' +
                        'stroke-dasharray="' + dashArr + '" stroke-dashoffset="' + dashOff + '" ' +
                        'transform="rotate(-90 ' + cx + ' ' + cy + ')" ' +
                        'class="donut-segment" data-index="' + k + '" />';

                    // Midpoint angle for percentage label
                    var midFraction = (accumulatedOffset + (segLength / 2)) / C;
                    var midAngle = (midFraction * 2 * Math.PI) - (Math.PI / 2);

                    var labelX = cx + r * Math.cos(midAngle);
                    var labelY = cy + r * Math.sin(midAngle) + 4;

                    // Large enough slices (>= 4.5%): label directly inside slice
                    if (pctVal >= 0.045) {
                        percentLabelsSvg += '<text x="' + labelX.toFixed(1) + '" y="' + labelY.toFixed(1) + '" ' +
                            'class="donut-segment-pct" text-anchor="middle" data-index="' + k + '">' + pctStr + '</text>';
                    }
                    // Smaller slices (1.0% - 4.4%): callout just outside slice perimeter
                    else if (pctVal >= 0.01) {
                        var outR = r + (strokeW / 2) + 12;
                        var outX = cx + outR * Math.cos(midAngle);
                        var outY = cy + outR * Math.sin(midAngle) + 3.5;
                        percentLabelsSvg += '<text x="' + outX.toFixed(1) + '" y="' + outY.toFixed(1) + '" ' +
                            'class="donut-segment-pct-out" text-anchor="middle" data-index="' + k + '">' + pctStr + '</text>';
                    }

                    accumulatedOffset += segLength;
                }

                tableRowsHtml +=
                    '<tr class="donut-table-row" data-index="' + k + '">' +
                        '<td class="td-type">' +
                            '<span class="donut-table-dot" style="background-color:' + segColor + ';"></span>' +
                            '<span class="donut-table-name" title="' + safeType + '">' + safeType + '</span>' +
                        '</td>' +
                        '<td class="td-qty">' + itm.qty.toLocaleString() + '</td>' +
                        '<td class="td-pct">' + pctStr + '</td>' +
                    '</tr>';
            }

            var subLabel = isFailed ? 'Failed Qty' : 'Passed Qty';
            var totalNumFontSize = total >= 100000 ? '20px' : '24px';
            var centerSvg =
                '<circle cx="' + cx + '" cy="' + cy + '" r="' + (r - strokeW / 2) + '" class="donut-center-bg" />' +
                '<g class="donut-center-group">' +
                    '<text x="' + cx + '" y="' + (cy - 16) + '" class="donut-center-label">TOTAL</text>' +
                    '<text x="' + cx + '" y="' + (cy + 7) + '" class="donut-center-num" style="font-size:' + totalNumFontSize + ';">' + total.toLocaleString() + '</text>' +
                    '<text x="' + cx + '" y="' + (cy + 25) + '" class="donut-center-sub">' + subLabel + '</text>' +
                '</g>';

            var cardClass = 'modern-donut-card' + (isFailed ? ' donut-failed-card' : '');
            var html =
                '<div class="' + cardClass + '">' +
                    '<div class="donut-visual-col">' +
                        '<svg class="donut-svg" viewBox="0 0 240 240">' +
                            svgPaths +
                            centerSvg +
                            percentLabelsSvg +
                        '</svg>' +
                    '</div>' +
                    '<div class="donut-table-col">' +
                        '<div class="donut-table-wrapper">' +
                            '<table class="donut-details-table">' +
                                '<thead>' +
                                    '<tr>' +
                                        '<th class="th-type">Battery Type</th>' +
                                        '<th class="th-qty">Qty</th>' +
                                        '<th class="th-pct">%</th>' +
                                    '</tr>' +
                                '</thead>' +
                                '<tbody>' +
                                    tableRowsHtml +
                                '</tbody>' +
                            '</table>' +
                        '</div>' +
                    '</div>' +
                    '<div class="donut-tooltip" style="display:none;"></div>' +
                '</div>';

            $container.html(html);

            // Tooltip & Hover Interactivity
            var $tooltip = $container.find('.donut-tooltip');

            function positionTooltip(e) {
                var cOffset = $container.offset();
                var x = e.pageX - cOffset.left + 14;
                var y = e.pageY - cOffset.top - 24;

                var ttW = $tooltip.outerWidth();
                var ttH = $tooltip.outerHeight();
                var cW = $container.width();
                var cH = $container.height();

                if (x + ttW > cW - 8) {
                    x = e.pageX - cOffset.left - ttW - 14;
                }
                if (x < 8) x = 8;
                if (y + ttH > cH - 8) {
                    y = cH - ttH - 8;
                }
                if (y < 8) y = 8;

                $tooltip.css({ left: x + 'px', top: y + 'px' });
            }

            $container.find('.donut-segment, .donut-table-row, .donut-segment-pct').on('mouseenter', function (e) {
                var idx = parseInt($(this).attr('data-index'), 10);
                if (isNaN(idx) || !items[idx]) return;

                var itm = items[idx];
                var col = palette[idx % palette.length];
                var pct = ((itm.qty / total) * 100).toFixed(1) + '%';
                var safeType = $('<div>').text(itm.type).html();

                $container.find('.donut-segment').each(function () {
                    if ($(this).attr('data-index') == idx) {
                        $(this).css({ 'stroke-width': '38px', 'opacity': '1' });
                    } else {
                        $(this).css({ 'stroke-width': '32px', 'opacity': '0.35' });
                    }
                });

                $container.find('.donut-table-row').removeClass('active');
                $container.find('.donut-table-row[data-index="' + idx + '"]').addClass('active');

                var ttContent =
                    '<div class="donut-tt-header">' +
                        '<span class="donut-tt-dot" style="background-color:' + col + ';"></span>' +
                        safeType +
                    '</div>' +
                    '<div class="donut-tt-body">' +
                        '<div><span class="donut-tt-lbl">Battery Type:</span><span class="donut-tt-val">' + safeType + '</span></div>' +
                        '<div><span class="donut-tt-lbl">Qty:</span><span class="donut-tt-val">' + itm.qty.toLocaleString() + '</span></div>' +
                        '<div><span class="donut-tt-lbl">Percentage:</span><span class="donut-tt-val">' + pct + '</span></div>' +
                    '</div>';

                $tooltip.html(ttContent).stop(true, true).fadeIn(120);
                positionTooltip(e);
            });

            $container.find('.donut-segment, .donut-table-row, .donut-segment-pct').on('mousemove', function (e) {
                positionTooltip(e);
            });

            $container.find('.donut-segment, .donut-table-row, .donut-segment-pct').on('mouseleave', function () {
                $container.find('.donut-segment').css({ 'stroke-width': '32px', 'opacity': '1' });
                $container.find('.donut-table-row').removeClass('active');
                $tooltip.stop(true, true).fadeOut(100);
            });
        }

        // Branch 1: Pre-computed or filtered items array
        if (Array.isArray(dataOrUrl)) {
            var res = sanitizeRecords(dataOrUrl);
            drawWithData(res.items, res.total);
            return;
        }

        // Branch 2: String URL endpoint
        if (typeof dataOrUrl === 'string') {
            $.ajax({
                type: "GET",
                url: dataOrUrl,
                dataType: "text",
                cache: false,
                success: function (response) {
                    var rawData = [];
                    try {
                        rawData = JSON.parse(response);
                        if (rawData && rawData.record && Array.isArray(rawData.record)) {
                            rawData = rawData.record;
                        }
                    } catch (e) {
                        rawData = [];
                    }

                    var res = sanitizeRecords(rawData);
                    drawWithData(res.items, res.total);
                },
                error: function () {
                    $container.html(
                        '<div class="donut-empty-container">' +
                            '<div class="donut-empty-icon"><i class="fa fa-pie-chart"></i></div>' +
                            '<div class="donut-empty-text">No data available</div>' +
                            '<div class="donut-empty-sub">Unable to load ' + (isFailed ? 'failed' : 'passed') + ' battery chart</div>' +
                        '</div>'
                    );
                }
            });
        }
    }

    // Expose globally
    window.renderModernDonutChart = renderModernDonutChart;

})(window, jQuery);
