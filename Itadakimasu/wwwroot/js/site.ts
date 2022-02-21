function reloadSelect(id: string) {
    //現状のURLからpageは抜いてリロード
    const value = (document.getElementById(id) as HTMLInputElement).value;
    const url = new URL(location.href);
    url.searchParams.delete('page');
    document.cookie = `${id}=${value};Path=/`;
    location.href = url.href;
}

// 「全て選択」チェックで全てにチェック付く
function allChecked(element: HTMLElement)  {
    const checked = (element as HTMLInputElement).checked;
    const checkbox = document.getElementsByName("checkbox");
    for (let i = 0; i < checkbox.length; i++) {
        (checkbox[i] as HTMLInputElement).checked = checked;
    }
}

function checkSync(element: HTMLElement) {
    const checked = (element as HTMLInputElement).checked;
    const eles = document.getElementsByClassName(element.className);
    Array.prototype.filter.call(eles, function (element) {
        const input = element as HTMLInputElement;
        input.checked = checked;
    });
}

function changeImage(name: string, nextName: string): void {
    (document.getElementById(name) as HTMLInputElement).checked = false;
    (document.getElementById(nextName) as HTMLInputElement).checked = true;
}