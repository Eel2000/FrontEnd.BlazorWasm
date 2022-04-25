
function setData(key, value) {
    localStorage.setItem(key, value);
    console.log('data cached in the local storage');
}

function getData(key) {
    let data = localStorage.getItem(key);
    return data;
}

function clearStorage() {
    localStorage.clear();
}