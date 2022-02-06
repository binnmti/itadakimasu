function reloadSelect(id) {
    //現状のURLからpageは抜いてリロード
    var value = document.getElementById(id).value;
    var url = new URL(location.href);
    url.searchParams.delete('page');
    document.cookie = "".concat(id, "=").concat(value, ";Path=/");
    location.href = url.href;
}
//# sourceMappingURL=site.js.map