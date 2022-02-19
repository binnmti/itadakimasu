function reloadSelect(id) {
    //現状のURLからpageは抜いてリロード
    var value = document.getElementById(id).value;
    var url = new URL(location.href);
    url.searchParams.delete('page');
    document.cookie = "".concat(id, "=").concat(value, ";Path=/");
    location.href = url.href;
}
// 「全て選択」チェックで全てにチェック付く
function allChecked(element) {
    var checked = element.checked;
    var checkbox = document.getElementsByName("checkbox");
    for (var i = 0; i < checkbox.length; i++) {
        checkbox[i].checked = checked;
    }
}
function checkSync(element) {
    var checked = element.checked;
    var eles = document.getElementsByClassName(element.className);
    Array.prototype.filter.call(eles, function (element) {
        var input = element;
        input.checked = checked;
    });
}
function changeImage(name, nextName) {
    document.getElementById(name).checked = false;
    document.getElementById(nextName).checked = true;
}
//# sourceMappingURL=site.js.map