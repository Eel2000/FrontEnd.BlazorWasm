
function setData(key, value) {
    console.log(`${key} \n ${value}`);
    localStorage.setItem(key, value);
    console.log('data cached in the local storage');
}

function getData(key) {
    let data = localStorage.getItem(key);
    console.log(`data found ${data}`);
    return data;
}

function clearStorage() {
    localStorage.clear();
    console.log("local storage cleaned");
}