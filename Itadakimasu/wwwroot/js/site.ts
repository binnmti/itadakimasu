function reloadSelect(id: string) {
    //現状のURLからpageは抜いてリロード
    const value = (document.getElementById(id) as HTMLInputElement).value;
    const url = new URL(location.href);
    url.searchParams.delete('page');
    document.cookie = `${id}=${value};Path=/`;
    location.href = url.href;
}

// 「全て選択」チェックで全てにチェック付く
function allChecked() {
    const check = (document.getElementById("check-all") as HTMLInputElement).checked;
    const checkbox = document.getElementsByName("checkbox");
    for (let i = 0; i < checkbox.length; i++) {
        (checkbox[i] as HTMLInputElement).checked = check;
    }
}
