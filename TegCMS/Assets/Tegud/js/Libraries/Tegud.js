﻿var TEGUD = {};

(function (d) { var e = d.c_ || {}; d.publish = function (a, b) { for (var c = e[a], f = c ? c.length : 0; f--;) c[f].apply(d, b || []) }; d.subscribe = function (a, b) { e[a] || (e[a] = []); e[a].push(b); return [a, b] }; d.unsubscribe = function (a) { for (var b = e[a[0]], a = a[1], c = b ? b.length : 0; c--;) b[c] === a && b.splice(c, 1) } })(TEGUD);
