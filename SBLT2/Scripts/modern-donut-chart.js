/**
 * Modern Enterprise Donut Chart Component
 * High-precision SVG donut renderer with interactive details table
 * Version: 3.0 - Fixed color rendering, thick ring, white slice separators
 */
(function (window, $) {
    'use strict';

    // Color palettes - exact reference design
    var passedPalette = [
        '#3B82F6', // Royal Blue (Reference design main slice)
        '#10B981', // Emerald Green (Reference design 2nd slice)
        '#F59E0B', // Golden Amber (Reference design 3rd slice)
        '#8B5CF6', // Purple (Reference design 4th slice)
        '#EC4899', // Pink
        '#06B6D4', // Cyan Teal
        '#14B8A6', // Dark Teal
        '#6366F1'  // Indigo
    ];

    var failedPalette = [
        '#F43F5E', // Coral / Rose Red (Reference design main slice)
        '#F59E0B', // Warm Amber (Reference design 2nd slice)
        '#8B5CF6', // Purple (Reference design 3rd slice)
        '#06B6D4', // Cyan Teal (Reference design 4th slice)
        '#EC4899', // Pink
        '#3B82F6', // Royal Blue
        '#DC2626', // Crimson
        '#D97706'  // Dark Amber
    ];

    /**
     * Parse raw API records into sorted [{type, qty}] array
     */
    function sanitizeRecords(rawList) {
        var items = [];
        var total = 0;
        if (!Array.isArray(rawList)) return { items: items, total: total };

        for (var i = 0; i < rawList.length; i++) {
            var r = rawList[i];
            if (!r) continue;
            var type = (r.label || r.mv_BatteryType || r.BatteryType || r.type || '').toString().trim();
            if (!type) type = 'Type ' + (i + 1);
            var qty = parseInt(r.y != null ? r.y : (r.Qty != null ? r.Qty : (r.qty != null ? r.qty : (r.TotalPassed != null ? r.TotalPassed : (r.TotalFailed != null ? r.TotalFailed : 0)))), 10);
            if (isNaN(qty) || qty <= 0) continue;
            items.push({ type: type, qty: qty });
            total += qty;
        }

        // Sort descending: highest qty first = first color in palette
        items.sort(function (a, b) { return b.qty - a.qty; });
        return { items: items, total: total };
    }

    /**
     * Main chart renderer
     * @param {string} containerSelector - CSS selector for chart container div
     * @param {string|Array} dataOrUrl   - API URL string OR pre-built items array
     * @param {string} chartType         - 'passed' or 'failed'
     */
    function renderModernDonutChart(containerSelector, dataOrUrl, chartType) {
        var $container = $(containerSelector);
        if (!$container.length) return;

        var isFailed = (chartType === 'failed');
        var palette   = isFailed ? failedPalette : passedPalette;

        // ----------------------------------------------------------------
        // Core draw function
        // ----------------------------------------------------------------
        function drawChart(items, total) {

            // No-data / empty state
            if (!items || items.length === 0 || total === 0) {
                $container.html(
                    '<div style="display:flex;flex-direction:column;align-items:center;justify-content:center;' +
                        'min-height:220px;width:100%;text-align:center;padding:30px 20px;">' +
                        '<i class="fa fa-pie-chart" style="font-size:42px;color:#CBD5E1;margin-bottom:12px;"></i>' +
                        '<div style="font-size:14px;font-weight:700;color:#64748B;margin-bottom:4px;">No data available</div>' +
                        '<div style="font-size:12px;color:#94A3B8;">No ' + (isFailed ? 'failed' : 'passed') +
                            ' batteries recorded for the selected period</div>' +
                    '</div>'
                );
                return;
            }

            // ---- SVG geometry ----
            // viewBox = 260x260, center = (130,130), ring radius = 82, ring thickness = 40
            var VB  = 260;   // viewBox size
            var cx  = 130;   // center x
            var cy  = 130;   // center y
            var r   = 82;    // ring radius (centerline)
            var sw  = 40;    // stroke-width (ring thickness)
            var C   = 2 * Math.PI * r;   // circumference ≈ 515.22

            // ---- Build SVG ----
            var svgInner = '';

            // 1. Track (background ring) - very light grey
            svgInner += '<circle cx="' + cx + '" cy="' + cy + '" r="' + r + '" ' +
                        'fill="none" stroke="#E2E8F0" stroke-width="' + sw + '" opacity="0.5"/>';

            // 2. Colored segments
            var accumulated = 0;
            for (var k = 0; k < items.length; k++) {
                var itm      = items[k];
                var pctVal   = itm.qty / total;
                var segLen   = pctVal * C;
                var color    = palette[k % palette.length];

                if (items.length === 1) {
                    // Single segment - full ring, no rotation needed
                    svgInner += '<circle cx="' + cx + '" cy="' + cy + '" r="' + r + '" ' +
                                'fill="none" stroke="' + color + '" stroke-width="' + sw + '" ' +
                                'class="donut-seg" data-index="' + k + '"/>';
                } else {
                    // dasharray: visible portion then gap to complete circumference
                    var dash = segLen.toFixed(3) + ' ' + (C - segLen).toFixed(3);
                    // dashoffset: negative = advance start point clockwise; rotate SVG -90° so 0° = top
                    var offset = (-accumulated).toFixed(3);

                    svgInner += '<circle cx="' + cx + '" cy="' + cy + '" r="' + r + '" ' +
                                'fill="none" stroke="' + color + '" stroke-width="' + sw + '" ' +
                                'stroke-dasharray="' + dash + '" stroke-dashoffset="' + offset + '" ' +
                                'transform="rotate(-90 ' + cx + ' ' + cy + ')" ' +
                                'stroke-linecap="butt" ' +
                                'class="donut-seg" data-index="' + k + '"/>';
                    accumulated += segLen;
                }
            }

            // 3. White divider lines between slices (drawn AFTER all segments)
            var acc2 = 0;
            if (items.length > 1) {
                for (var d = 0; d < items.length; d++) {
                    var pct2    = items[d].qty / total;
                    var sLen2   = pct2 * C;
                    // angle of slice boundary in SVG coords (starting at top = -90deg)
                    var angle   = (-Math.PI / 2) + (acc2 / C) * 2 * Math.PI;
                    var rInner  = r - sw / 2 - 1;
                    var rOuter  = r + sw / 2 + 1;
                    var x1      = cx + rInner * Math.cos(angle);
                    var y1      = cy + rInner * Math.sin(angle);
                    var x2      = cx + rOuter * Math.cos(angle);
                    var y2      = cy + rOuter * Math.sin(angle);

                    svgInner += '<line x1="' + x1.toFixed(2) + '" y1="' + y1.toFixed(2) + '" ' +
                                'x2="' + x2.toFixed(2) + '" y2="' + y2.toFixed(2) + '" ' +
                                'stroke="#ffffff" stroke-width="2.5" stroke-linecap="butt" ' +
                                'pointer-events="none"/>';
                    acc2 += sLen2;
                }
            }

            // 4. Center hole — pure white fill with clean shadow ring (matches reference design 2)
            var holeR    = r - sw / 2 - 1;
            var subLabel = isFailed ? 'Total Failed' : 'Total Passed';
            var numFS    = total >= 10000 ? '22' : '26';

            // Chart accent color for subtitle (green for passed, red for failed)
            var accentColor = isFailed ? '#EF4444' : '#10B981';

            // Detect dark mode
            var isDark = $('body').hasClass('dark-theme') || $('html').hasClass('dark-theme') ||
                         $('[class*="dark-theme"]').length > 0;
            var holeFill = isDark ? '#1E293B' : '#ffffff';
            var numFill  = isDark ? '#F1F5F9'  : '#0F172A';

            // Shadow ring around hole for depth
            svgInner += '<circle cx="' + cx + '" cy="' + cy + '" r="' + (holeR + 1) + '" fill="none" ' +
                        'stroke="' + accentColor + '" stroke-width="2" stroke-opacity="0.18"/>';
            // White hole
            svgInner += '<circle cx="' + cx + '" cy="' + cy + '" r="' + holeR + '" fill="' + holeFill + '"/>';
            // TOTAL label
            svgInner += '<text x="' + cx + '" y="' + (cy - 14) + '" text-anchor="middle" ' +
                        'font-family="Roboto,Arial,sans-serif" font-size="10" font-weight="700" ' +
                        'fill="#94A3B8" letter-spacing="2">TOTAL</text>';
            // Big bold number
            svgInner += '<text x="' + cx + '" y="' + (cy + 12) + '" text-anchor="middle" ' +
                        'font-family="Roboto,Arial,sans-serif" font-size="' + numFS + '" font-weight="900" ' +
                        'fill="' + numFill + '">' + total.toLocaleString() + '</text>';
            // Colored subtitle (Total Passed / Total Failed)
            svgInner += '<text x="' + cx + '" y="' + (cy + 30) + '" text-anchor="middle" ' +
                        'font-family="Roboto,Arial,sans-serif" font-size="11" font-weight="600" ' +
                        'fill="' + accentColor + '">' + subLabel + '</text>';

            // 5. Percentage labels directly on slice midpoints — larger & bold (matches reference design 2)
            var acc3 = 0;
            for (var p = 0; p < items.length; p++) {
                var pct3   = items[p].qty / total;
                var sLen3  = pct3 * C;
                var midAng = (-Math.PI / 2) + ((acc3 + sLen3 / 2) / C) * 2 * Math.PI;
                var pctStr = Math.round(pct3 * 100) + '%';

                if (pct3 >= 0.05) {
                    // Large label directly on the slice ring
                    var lx = cx + r * Math.cos(midAng);
                    var ly = cy + r * Math.sin(midAng) + 5;
                    svgInner += '<text x="' + lx.toFixed(1) + '" y="' + ly.toFixed(1) + '" text-anchor="middle" ' +
                                'font-family="Roboto,Arial,sans-serif" font-size="13" font-weight="800" ' +
                                'fill="#ffffff" pointer-events="none" data-index="' + p + '">' + pctStr + '</text>';
                } else if (pct3 >= 0.02) {
                    // Small callout outside for tiny slices
                    var outR = r + sw / 2 + 20;
                    var ox   = cx + outR * Math.cos(midAng);
                    var oy   = cy + outR * Math.sin(midAng) + 4;
                    svgInner += '<text x="' + ox.toFixed(1) + '" y="' + oy.toFixed(1) + '" text-anchor="middle" ' +
                                'font-family="Roboto,Arial,sans-serif" font-size="11" font-weight="700" ' +
                                'fill="#475569" pointer-events="none" data-index="' + p + '">' + pctStr + '</text>';
                }
                acc3 += sLen3;
            }

            // ---- Build legend table rows ----
            var tableRows = '';
            for (var t = 0; t < items.length; t++) {
                var color2   = palette[t % palette.length];
                var pct4     = (items[t].qty / total * 100).toFixed(1) + '%';
                var safeName = $('<div>').text(items[t].type).html();
                tableRows +=
                    '<tr class="donut-table-row" data-index="' + t + '" ' +
                        'style="border-bottom:1px solid #F1F5F9;cursor:pointer;transition:background 0.15s;">' +
                        '<td style="padding:9px 4px;vertical-align:middle;">' +
                            '<span style="display:inline-block;width:10px;height:10px;border-radius:50%;' +
                                'background:' + color2 + ';margin-right:10px;vertical-align:middle;"></span>' +
                            '<span title="' + safeName + '" style="font-weight:600;font-size:13px;color:#1E293B;' +
                                'vertical-align:middle;max-width:120px;display:inline-block;' +
                                'white-space:nowrap;overflow:hidden;text-overflow:ellipsis;">' + safeName + '</span>' +
                        '</td>' +
                        '<td style="padding:9px 4px;text-align:right;font-weight:800;font-size:13.5px;color:#0F172A;' +
                            'font-variant-numeric:tabular-nums;">' + items[t].qty.toLocaleString() + '</td>' +
                        '<td style="padding:9px 4px;text-align:right;font-weight:500;font-size:12px;color:#64748B;' +
                            'font-variant-numeric:tabular-nums;">' + pct4 + '</td>' +
                    '</tr>';
            }

            // ---- Assemble final HTML ----
            var cardClass = isFailed ? 'modern-donut-card donut-failed-card' : 'modern-donut-card';

            var html =
                '<div class="' + cardClass + '" style="position:relative;display:flex;flex-direction:row;' +
                    'align-items:center;width:100%;min-height:240px;gap:16px;box-sizing:border-box;">' +

                    // SVG column
                    '<div style="flex:0 0 230px;width:230px;height:230px;display:flex;align-items:center;justify-content:center;">' +
                        '<svg viewBox="0 0 ' + VB + ' ' + VB + '" ' +
                            'style="width:100%;height:100%;overflow:visible;" ' +
                            'xmlns="http://www.w3.org/2000/svg">' +
                            svgInner +
                        '</svg>' +
                    '</div>' +

                    // Legend table column
                    '<div style="flex:1 1 auto;min-width:200px;display:flex;flex-direction:column;justify-content:center;">' +
                        '<div style="width:100%;max-height:280px;overflow-y:auto;overflow-x:hidden;' +
                            'background:#F8FAFC;border:1px solid #E2E8F0;border-radius:12px;' +
                            'padding:12px 16px;box-sizing:border-box;">' +
                            '<table style="width:100%;border-collapse:collapse;font-size:12.5px;text-align:left;">' +
                                '<thead>' +
                                    '<tr>' +
                                        '<th style="position:sticky;top:0;background:#F8FAFC;z-index:2;' +
                                            'padding:6px 4px 10px;font-size:12px;font-weight:600;' +
                                            'color:#64748B;border-bottom:1px solid #E2E8F0;text-align:left;width:48%;">Battery Type</th>' +
                                        '<th style="position:sticky;top:0;background:#F8FAFC;z-index:2;' +
                                            'padding:6px 4px 10px;font-size:12px;font-weight:600;' +
                                            'color:#64748B;border-bottom:1px solid #E2E8F0;text-align:right;width:26%;">Qty</th>' +
                                        '<th style="position:sticky;top:0;background:#F8FAFC;z-index:2;' +
                                            'padding:6px 4px 10px;font-size:12px;font-weight:600;' +
                                            'color:#64748B;border-bottom:1px solid #E2E8F0;text-align:right;width:26%;">%</th>' +
                                    '</tr>' +
                                '</thead>' +
                                '<tbody>' + tableRows + '</tbody>' +
                            '</table>' +
                        '</div>' +
                    '</div>' +

                    // Tooltip (hidden)
                    '<div class="donut-tooltip" style="display:none;position:absolute;pointer-events:none;' +
                        'z-index:1050;padding:10px 14px;background:#fff;color:#0f172a;border-radius:8px;' +
                        'font-size:12px;box-shadow:0 10px 25px rgba(0,0,0,0.15);border:1px solid #e2e8f0;' +
                        'white-space:nowrap;min-width:160px;"></div>' +
                '</div>';

            $container.html(html);

            // ---- Hover interactions ----
            var $tooltip = $container.find('.donut-tooltip');

            $container.find('.donut-seg, .donut-table-row').on('mouseenter', function (e) {
                var idx = parseInt($(this).attr('data-index'), 10);
                if (isNaN(idx) || !items[idx]) return;

                var itm    = items[idx];
                var col    = palette[idx % palette.length];
                var pct    = (itm.qty / total * 100).toFixed(1) + '%';
                var name   = $('<div>').text(itm.type).html();

                $container.find('.donut-seg').each(function () {
                    var isActive = $(this).attr('data-index') == idx;
                    this.setAttribute('stroke-width', isActive ? (sw + 6) : sw);
                    this.style.opacity = isActive ? '1' : '0.35';
                });

                $container.find('.donut-table-row').each(function () {
                    $(this).css('background', $(this).attr('data-index') == idx
                        ? (isFailed ? 'rgba(239,68,68,0.08)' : 'rgba(16,185,129,0.08)')
                        : '');
                });

                $tooltip.html(
                    '<div style="display:flex;align-items:center;gap:8px;margin-bottom:6px;' +
                        'padding-bottom:5px;border-bottom:1px solid rgba(148,163,184,0.25);' +
                        'font-size:13px;font-weight:700;">' +
                        '<span style="width:8px;height:8px;border-radius:50%;background:' + col + ';flex-shrink:0;display:inline-block;"></span>' +
                        name +
                    '</div>' +
                    '<div style="display:flex;flex-direction:column;gap:3px;">' +
                        '<div><span style="color:#64748B;margin-right:6px;">Battery Type:</span><span style="font-weight:700;">' + name + '</span></div>' +
                        '<div><span style="color:#64748B;margin-right:6px;">Qty:</span><span style="font-weight:700;">' + itm.qty.toLocaleString() + '</span></div>' +
                        '<div><span style="color:#64748B;margin-right:6px;">Percentage:</span><span style="font-weight:700;">' + pct + '</span></div>' +
                    '</div>'
                ).stop(true, true).fadeIn(120);

                var cOff = $container.offset();
                var tx = e.pageX - cOff.left + 14;
                var ty = e.pageY - cOff.top  - 24;
                if (tx + 180 > $container.width())  tx = e.pageX - cOff.left - 194;
                if (tx < 8) tx = 8;
                if (ty < 8) ty = 8;
                $tooltip.css({ left: tx + 'px', top: ty + 'px' });
            });

            $container.find('.donut-seg, .donut-table-row').on('mousemove', function (e) {
                var cOff = $container.offset();
                var tx = e.pageX - cOff.left + 14;
                var ty = e.pageY - cOff.top  - 24;
                if (tx + 180 > $container.width())  tx = e.pageX - cOff.left - 194;
                if (tx < 8) tx = 8;
                if (ty < 8) ty = 8;
                $tooltip.css({ left: tx + 'px', top: ty + 'px' });
            });

            $container.find('.donut-seg, .donut-table-row').on('mouseleave', function () {
                $container.find('.donut-seg').each(function () {
                    this.setAttribute('stroke-width', sw);
                    this.style.opacity = '1';
                });
                $container.find('.donut-table-row').css('background', '');
                $tooltip.stop(true, true).fadeOut(100);
            });
        } // end drawChart

        // ----------------------------------------------------------------
        // Data loading
        // ----------------------------------------------------------------
        if (Array.isArray(dataOrUrl)) {
            // Pre-computed array (e.g. from Summary Report)
            var res = sanitizeRecords(dataOrUrl);
            drawChart(res.items, res.total);
            return;
        }

        if (typeof dataOrUrl === 'string') {
            $.ajax({
                type: 'GET',
                url: dataOrUrl,
                dataType: 'text',
                cache: false,
                success: function (response) {
                    var raw = [];
                    try {
                        raw = JSON.parse(response);
                        if (raw && raw.record && Array.isArray(raw.record)) raw = raw.record;
                    } catch (e) { raw = []; }
                    var res2 = sanitizeRecords(raw);
                    drawChart(res2.items, res2.total);
                },
                error: function () {
                    drawChart([], 0);
                }
            });
        }
    }

    // Expose globally
    window.renderModernDonutChart = renderModernDonutChart;

})(window, jQuery);
