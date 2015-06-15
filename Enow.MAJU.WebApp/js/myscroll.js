var myScroll,
	pullDownEl, pullDownOffset,
	pullUpEl, pullUpOffset,
	pagedown = 1, pageup = 2;

/**
* 下拉刷新 （自定义实现此方法）
* myScroll.refresh();		// 数据加载完成后，调用界面更新方法
*/
function pullDownAction() {
}

/**
* 滚动翻页 （自定义实现此方法）
* myScroll.refresh();		// 数据加载完成后，调用界面更新方法
*/
function pullUpAction() {
}

/**
* 初始化iScroll控件
*/
function loaded() {
    if (myScroll != null) {
//        alert(myScroll);
        myScroll.destroy();
    }
    pullDownEl = document.getElementById('pullDown');
    pullDownOffset = pullDownEl.offsetHeight;
    pullUpEl = document.getElementById('pullUp');
    pullUpOffset = pullUpEl.offsetHeight;
    pullUpEl.style.display = 'none';
    pullDownEl.style.display = 'none';
    setTimeout(function () {
        myScroll = new iScroll('wrapper', {
            scrollbarClass: 'myScrollbar', /* 重要样式 */
            useTransition: false, /* 此属性不知用意，本人从true改为false */
            checkDOMChange: false,
            topOffset: pullDownOffset,
            zoom: true,
            onRefresh: function () {
                if (pullDownEl.className.match('loading')) {
                    pullDownEl.className = '';
                    pullDownEl.querySelector('.pullDownLabel').innerHTML = '下拉刷新...';
                    pullDownEl.style.display = 'none';
                } else if (pullUpEl.className.match('loading')) {
                    pullUpEl.className = '';
                    pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
                    pullUpEl.style.display = 'none';

                }
            },
            onScrollMove: function () {
                if (this.y > 5 && !pullDownEl.className.match('flip')) {
                    pullDownEl.style.display = 'block';
                    pullDownEl.className = 'flip';
                    pullDownEl.querySelector('.pullDownLabel').innerHTML = '松手开始更新...';
                    this.minScrollY = 0;
                } else if (this.y < 5 && pullDownEl.className.match('flip')) {
                    pullDownEl.style.display = 'block';
                    pullDownEl.className = '';
                    pullDownEl.querySelector('.pullDownLabel').innerHTML = '下拉刷新...';
                    this.minScrollY = -pullDownOffset;
                } else if (this.y < (this.maxScrollY - 5) && !pullUpEl.className.match('flip')) {
                    pullUpEl.style.display = 'block';
                    pullUpEl.className = 'flip';
                    pullUpEl.querySelector('.pullUpLabel').innerHTML = '松手开始更新...';
                    this.maxScrollY = this.maxScrollY;
                } else if (this.y > (this.maxScrollY + 5) && pullUpEl.className.match('flip')) {
                    pullUpEl.style.display = 'block';
                    pullUpEl.className = '';
                    pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
                    this.maxScrollY = this.maxScrollY;
                }
            },
            onScrollEnd: function () {
                if (pullDownEl.className.match('flip')) {
                    pullDownEl.className = 'loading';
                    pullDownEl.querySelector('.pullDownLabel').innerHTML = '加载中...';
                    pullDownAction(); // Execute custom function (ajax call?)
                    pageup = 2;
                } else if (pullUpEl.className.match('flip')) {
                    pullUpEl.className = 'loading';
                    pullUpEl.querySelector('.pullUpLabel').innerHTML = '加载中...';
                    pullUpAction(); // Execute custom function (ajax call?)
                }
            }
        });
    }, 1000);

    setTimeout(function () { document.getElementById('wrapper').style.left = '0'; }, 800);
}