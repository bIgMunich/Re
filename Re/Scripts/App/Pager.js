var paerSpance = {
    getpager: function (self, p, func) {
        var strhtml = "";
        var currentIndex = self.CurrenetPageIndex;
        if (self.PageCount > 0 && self.PageCount <= 10) {
            for (var i = 1; i <= self.PageCount; i++) {
                if (self.CurrenetPageIndex == i) {
                    strhtml += "<li class=\"active\"><a href=\"javascript:\" data-pc=\"" + i + "\">" + i + "</a></li>";
                } else {
                    strhtml += "<li><a href=\"javascript:\" data-pc=\"" + i + "\">" + i + "</a></li>";
                }
            }
        }
        if (self.PageCount > 10) {
            var beginIndex = 1;
            var endIndex = self.PageCount;
            if (currentIndex - 5 > 0) {
                beginIndex = currentIndex - 5;
            }
            if (currentIndex + 5 <= self.PageCount) {
                endIndex = currentIndex + 5;
            }
            for (var i = beginIndex; i <= endIndex; i++) {
                if (self.CurrenetPageIndex == i) {
                    strhtml += "<li class=\"active\"><a href=\"javascript:\" data-pc=\"" + i + "\">" + i + "</a></li>";
                } else {
                    strhtml += "<li><a href=\"javascript:\" data-pc=\"" + i + "\">" + i + "</a></li>";
                }
            }
        }
        $(".pagination").html(strhtml);
        $(".pagination li a").click(function () {
            if (p == undefined) {
                p = {};
                p.page = $(this).data("pc");
                func(p);
            } else {
                p.page = $(this).data("pc");
                func(p);
            }
        });
    }
}
