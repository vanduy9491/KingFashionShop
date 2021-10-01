(function ($) {
    "use strict";

    /*[ Load page ]
    ===========================================================*/
    $(".animsition").animsition({
        inClass: 'fade-in',
        outClass: 'fade-out',
        inDuration: 1500,
        outDuration: 800,
        linkElement: '.animsition-link',
        loading: true,
        loadingParentElement: 'html',
        loadingClass: 'animsition-loading-1',
        loadingInner: '<div class="loader05"></div>',
        timeout: false,
        timeoutCountdown: 5000,
        onLoadEvent: true,
        browser: ['animation-duration', '-webkit-animation-duration'],
        overlay: false,
        overlayClass: 'animsition-overlay-slide',
        overlayParentElement: 'html',
        transition: function (url) {
            window.location.href = url;
        }
    });

    /*[ Back to top ]
    ===========================================================*/
    var windowH = $(window).height() / 2;

    $(window).on('scroll', function () {
        if ($(this).scrollTop() > windowH) {
            $("#myBtn").css('display', 'flex');
        } else {
            $("#myBtn").css('display', 'none');
        }
    });

    $('#myBtn').on("click", function () {
        $('html, body').animate({
            scrollTop: 0
        }, 300);
    });


    /*==================================================================
    [ Fixed Header ]*/
    var headerDesktop = $('.container-menu-desktop');
    var wrapMenu = $('.wrap-menu-desktop');

    if ($('.top-bar').length > 0) {
        var posWrapHeader = $('.top-bar').height();
    } else {
        var posWrapHeader = 0;
    }


    if ($(window).scrollTop() > posWrapHeader) {
        $(headerDesktop).addClass('fix-menu-desktop');
        $(wrapMenu).css('top', 0);
    } else {
        $(headerDesktop).removeClass('fix-menu-desktop');
        $(wrapMenu).css('top', posWrapHeader - $(this).scrollTop());
    }

    $(window).on('scroll', function () {
        if ($(this).scrollTop() > posWrapHeader) {
            $(headerDesktop).addClass('fix-menu-desktop');
            $(wrapMenu).css('top', 0);
        } else {
            $(headerDesktop).removeClass('fix-menu-desktop');
            $(wrapMenu).css('top', posWrapHeader - $(this).scrollTop());
        }
    });


    /*==================================================================
    [ Menu mobile ]*/
    $('.btn-show-menu-mobile').on('click', function () {
        $(this).toggleClass('is-active');
        $('.menu-mobile').slideToggle();
    });

    var arrowMainMenu = $('.arrow-main-menu-m');

    for (var i = 0; i < arrowMainMenu.length; i++) {
        $(arrowMainMenu[i]).on('click', function () {
            $(this).parent().find('.sub-menu-m').slideToggle();
            $(this).toggleClass('turn-arrow-main-menu-m');
        })
    }

    $(window).resize(function () {
        if ($(window).width() >= 992) {
            if ($('.menu-mobile').css('display') == 'block') {
                $('.menu-mobile').css('display', 'none');
                $('.btn-show-menu-mobile').toggleClass('is-active');
            }

            $('.sub-menu-m').each(function () {
                if ($(this).css('display') == 'block') {
                    console.log('hello');
                    $(this).css('display', 'none');
                    $(arrowMainMenu).removeClass('turn-arrow-main-menu-m');
                }
            });

        }
    });


    /*==================================================================
    [ Show / hide modal search ]*/
    $('.js-show-modal-search').on('click', function () {
        $('.modal-search-header').addClass('show-modal-search');
        $(this).css('opacity', '0');
    });

    $('.js-hide-modal-search').on('click', function () {
        $('.modal-search-header').removeClass('show-modal-search');
        $('.js-show-modal-search').css('opacity', '1');
    });

    $('.container-search-header').on('click', function (e) {
        e.stopPropagation();
    });


    /*==================================================================
    [ Isotope ]*/
    var $topeContainer = $('.isotope-grid');
    var $filter = $('.filter-tope-group');

    // filter items on button click
    $filter.each(function () {
        $filter.on('click', 'button', function () {
            var filterValue = $(this).attr('data-filter');
            $topeContainer.isotope({
                filter: filterValue
            });
        });

    });
    var $grid;
    // init Isotope
    $(window).on('load', function () {
        $grid = $topeContainer.each(function () {
            $(this).isotope({
                itemSelector: '.isotope-item',
                layoutMode: 'masonry',
                percentPosition: true,
                animationEngine: 'best-available',
                masonry: {
                    columnWidth: '.isotope-item'
                }
            });
        });
    });

    var isotopeButton = $('.filter-tope-group button');

    $(isotopeButton).each(function () {
        $(this).on('click', function () {
            for (var i = 0; i < isotopeButton.length; i++) {
                $(isotopeButton[i]).removeClass('how-active1');
            }

            $(this).addClass('how-active1');
        });
    });

    /*==================================================================
    [ Filter / Search product ]*/
    $('.js-show-filter').on('click', function () {
        $(this).toggleClass('show-filter');
        $('.panel-filter').slideToggle(400);

        if ($('.js-show-search').hasClass('show-search')) {
            $('.js-show-search').removeClass('show-search');
            $('.panel-search').slideUp(400);
        }
    });

    $('.js-show-search').on('click', function () {
        $(this).toggleClass('show-search');
        $('.panel-search').slideToggle(400);

        if ($('.js-show-filter').hasClass('show-filter')) {
            $('.js-show-filter').removeClass('show-filter');
            $('.panel-filter').slideUp(400);
        }
    });


    /*==================================================================
    [ Cart ]*/
    $('.js-show-cart').on('click', function () {
        cartService.cartPanel();

    });

    $('.js-hide-cart').on('click', function () {
        $('.js-panel-cart').removeClass('show-header-cart');
    });

    /*==================================================================
    [ Cart ]*/
    $('.js-show-sidebar').on('click', function () {
        $('.js-sidebar').addClass('show-sidebar');
    });

    $('.js-hide-sidebar').on('click', function () {
        $('.js-sidebar').removeClass('show-sidebar');
    });



    /*==================================================================
    [ Rating ]*/
    $('.wrap-rating').each(function () {
        var item = $(this).find('.item-rating');
        var rated = -1;
        var input = $(this).find('input');
        $(input).val(0);

        $(item).on('mouseenter', function () {
            var index = item.index(this);
            var i = 0;
            for (i = 0; i <= index; i++) {
                $(item[i]).removeClass('zmdi-star-outline');
                $(item[i]).addClass('zmdi-star');
            }

            for (var j = i; j < item.length; j++) {
                $(item[j]).addClass('zmdi-star-outline');
                $(item[j]).removeClass('zmdi-star');
            }
        });

        $(item).on('click', function () {
            var index = item.index(this);
            rated = index;
            $(input).val(index + 1);
        });

        $(this).on('mouseleave', function () {
            var i = 0;
            for (i = 0; i <= rated; i++) {
                $(item[i]).removeClass('zmdi-star-outline');
                $(item[i]).addClass('zmdi-star');
            }

            for (var j = i; j < item.length; j++) {
                $(item[j]).addClass('zmdi-star-outline');
                $(item[j]).removeClass('zmdi-star');
            }
        });
    });








    /*==================================================================
   [ Show modal1 ]*/
    $.quickView = function () {
        $('.js-show-modal1').on('click', function (e) {
            e.preventDefault();
            let productId = $(this).attr('data-item');
            $.ajax({
                url: "https://localhost:44322/api/Product/GetProductBy?productId=" + productId,
                method: "GET",
                success: function (data) {
                    $(".js-name-detail").text(data.title);
                    $(".mtext-106").text("$" + data.price);


                    $('.slick3').empty();
                    $('.wrap-slick3-dots').empty();
                    $('.wrap-slick3-arrows').empty();
                    $('.slick3').attr("class", "slick3 gallery-lb");
                    $.each(data.images, function (index, item) {
                        $('.slick3').append(
                            `<div class="item-slick3" data-thumb="${item}">
                        <div class="wrap-pic-w pos-relative">
                            <img src="../images/${item}" alt="IMG-PRODUCT">
     
                            <a class="flex-c-m size-108 how-pos1 bor0 fs-16 cl10 bg0 hov-btn3 trans-04" href="${item}">
                                <i class="fa fa-expand"></i>
                            </a>
                        </div>
                    </div>
                    `);

                    });
                    $('.wrap-slick3').each(function () {
                        $(this).find('.slick3').slick({
                            slidesToShow: 1,
                            slidesToScroll: 1,
                            fade: true,
                            infinite: true,
                            autoplay: false,
                            autoplaySpeed: 6000,

                            arrows: true,
                            appendArrows: $(this).find('.wrap-slick3-arrows'),
                            prevArrow: '<button class="arrow-slick3 prev-slick3"><i class="fa fa-angle-left" aria-hidden="true"></i></button>',
                            nextArrow: '<button class="arrow-slick3 next-slick3"><i class="fa fa-angle-right" aria-hidden="true"></i></button>',

                            dots: true,
                            appendDots: $(this).find('.wrap-slick3-dots'),
                            dotsClass: 'slick3-dots',
                            customPaging: function (slick, index) {
                                var portrait = $(slick.$slides[index]).data('thumb');
                                return '<img src="../images/' + portrait + '"/><div class="slick3-dot-overlay"></div>';
                            },
                        });
                    });
                    $('.js-modal1').addClass('show-modal1');
                    $('.js-addcart-detail').attr("data-item", data.id);
                }
            });
        });
    }
    $.quickView();


    $('.js-hide-modal1').on('click', function () {
        $('.js-modal1').removeClass('show-modal1');
    });

    $.boundary = function () {
        var elementsSize = 0;
        var iterate = document.evaluate('//div[contains(@class,"isotope-item") and not(contains(@style,"display: none"))]', document.cloneNode(true));

        while (iterate.iterateNext() !== null) {
            elementsSize++;
        }
        return elementsSize
    }
    $.loadingMore = false;

    /*==================================================================
    [ Show modal1 ]*/
    $('#loadmore').on('click', function (e) {
        e.preventDefault();
        $('#loadmore a').text("Loading");
        $.loadingMore = true;
        var topCategoryUrl = "https://localhost:44322/api/Product/GetProductsTopCategory?limit=4";
        let topCategoryId = $('.how-active1').attr('data-item')
        if (topCategoryId !== undefined) {
            topCategoryUrl += `&topCategoryId=${topCategoryId}`;
        }
        topCategoryUrl += `&boundary=${$.boundary()}`;
        var dataFilter = $('.how-active1').attr('data-filter');
        dataFilter = dataFilter != '*' ? dataFilter.replace(".", "") : "";
        $.ajax({
            url: topCategoryUrl,
            method: "GET",
            error: function (xhr, status, errorThrown) {
                $.loadingMore = false;
                $('#loadmore a').text("Load More");
            },
            success: function (data) {
                $.loadingMore = false;
                $('#loadmore a').text("Load More");
                //if (data.length < 4) {
                //    document.getElementById('loadmore').style.visibility = 'hidden';
                //}
                $.each(data, function (index, product) {
                    var item = $(
                        `<div class="col-sm-6 col-md-4 col-lg-3 p-b-35 isotope-item ${dataFilter}">
                            <!-- Block2 -->
                            <div class="block2">
                                <div class="block2-pic hov-img0">
                                    <img src="images/${product.MainPhoto}" alt="IMG-PRODUCT">
                
                                    <a href="#" data-item="${product.id}" class="block2-btn flex-c-m stext-103 cl2 size-102 bg0 bor2 hov-btn1 p-lr-15 trans-04 js-show-modal1">
                                        Quick View
                                    </a>
                                </div>
                                <div class="block2-txt flex-w flex-t p-t-14">
                                    <div class="block2-txt-child1 flex-col-l ">
                                        <a href="product-detail.html" class="stext-104 cl4 hov-cl1 trans-04 js-name-b2 p-b-6">
                                        ${product.title}
                                        </a>
                                        <span class="stext-105 cl3">
                                            ${product.price}
                                        </span>
                                    </div>
                                    <div class="block2-txt-child2 flex-r p-t-3">
                                        <a href="#" class="btn-addwish-b2 dis-block pos-relative js-addwish-b2">
                                            <img class="icon-heart1 dis-block trans-04" src="images/icons/icon-heart-01.png" alt="ICON">
                                            <img class="icon-heart2 dis-block trans-04 ab-t-l" src="images/icons/icon-heart-02.png" alt="ICON">
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>`
                    );
                    $('.isotope-grid').append(item).isotope('appended', item);
                });
                $.quickView();
            }
        });
    });



    $('.block2 .block2-txt .stext-104').on('click', function (e) {
        e.preventDefault();
        let productId = $(this).attr('data-item');
        $.ajax({
            url: "https://localhost:44322/api/Product/GetProductBy?productId=" + productId,
            method: "GET",
            success: function (data) {
                $(".js-name-detail").text(data.title);
                $(".mtext-106").text("$" + data.price);
                $('.slick3').empty();
                $('.wrap-slick3-dots').empty();
                $('.wrap-slick3-arrows').empty();
                $('.slick3').attr("class", "slick3 gallery-lb");
                $.each(data.images, function (index, item) {
                    $('.slick3').append(
                        `<div class="item-slick3" data-thumb="${item}">
                        <div class="wrap-pic-w pos-relative">
                            <img src="../images/${item}" alt="IMG-PRODUCT">
                            <a class="flex-c-m size-108 how-pos1 bor0 fs-16 cl10 bg0 hov-btn3 trans-04" href="${item}">
                                <i class="fa fa-expand"></i>
                            </a>
                        </div>
                    </div>
                    `);

                });
                $('.js-modal1').addClass('show-modal1');
                $('.js-addcart-detail').attr("data-item", data.id);
            }
        });
    });
})(jQuery);