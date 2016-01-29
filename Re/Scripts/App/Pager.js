var preViewIndex;
var nextIndex;
var indexFlag = 0;
function init(pagerCount, currentPager) {
    if (currentPager == 0) {
        preViewIndex = 0;
    } else {
        preViewIndex = parseInt(currentPager) - 1;
    }
    if (pagerCount > currentPager) {
        nextIndex = parseInt(currentPager) + 1;
    } else {
        nextIndex = pagerCount;
        indexFlag = 1;
    }
    var ul = document.getElementById("ul");
    var preli = document.createElement("li");
    preli.innerHTML = '<a id="preview" href="/Home/Index?CurrentIndex=' + preViewIndex + '">preview</a>';
    ul.appendChild(preli);
    var currentli = document.createElement("li");
    currentli.innerHTML = '<a   href="#">当前第' + currentPager + '</a>';
    ul.appendChild(currentli);
    var nextli = document.createElement("li");
    nextli.innerHTML = '<a id="next" href="/Home/Index?CurrentIndex=' + nextIndex + '">next</a>';
    ul.appendChild(nextli);
}

$("#preview").click(function () {
    if (preViewIndex == 0) {
        this.style.backgroundColor = "red";
    }
    if (indexFlag == 1) {
        $("#next").style.backgroundColor = "red";
    }
});

$("#next").click(function () {
    if (preViewIndex == 0) {
        $("#preview").style.backgroundColor = "red";
    }
    if (indexFlag == 1) {
        this.style.backgroundColor = "red";
    }
});


