function reloadSelect(id: string) {
    //現状のURLからpageは抜いてリロード
    const value = (document.getElementById(id) as HTMLInputElement).value;
    const url = new URL(location.href);
    url.searchParams.delete('page');
    document.cookie = `${id}=${value};Path=/`;
    location.href = url.href;
}
