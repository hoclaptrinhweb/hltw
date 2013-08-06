<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucRss.ascx.cs"
    Inherits="usercontrols_ucRss" %>
<link href="<%= CurrentPage.UrlRoot %>/css/news.css" rel="stylesheet" type="text/css" />
<link href="<%= CurrentPage.UrlRoot %>/css/styleSheet.css" rel="stylesheet" type="text/css" />
<div class="box_outer">
    <div class="cat_article" itemscope itemtype="http://schema.org/Article">
        <h1 class="cat_article_title" itemprop="name">
            <a>RSS là gì?</a>
        </h1>
        <div class="article_meta">
            <span class="meta_author">Posted by: <a>Hoclaptrinhweb</a></span>
        </div>
        <div class="brief" itemprop="description">
            <asp:Literal ID="lbBrief" runat="server"></asp:Literal>
        </div>
        <div id="article_content" class="single_article_content" itemprop="articleBody">
            <p style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                background-color: rgb(255, 255, 255); color: rgb(85, 85, 85); font-family: Arial, Tahoma;
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: 16px; orphans: 2; text-align: start; text-indent: 0px; text-transform: none;
                white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto;
                -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;">
                Khi số lượng website tin tức ngày càng nhiều, việc duyệt Web để tìm những thông
                tin bạn cần ngày càng mất nhiều thời gian. Liệu có tốt hơn không nếu các thông tin
                và dữ liệu mới nhất được gửi trực tiếp đến bạn, thay vì bạn phải tự dò tìm thông
                tin từ trang web này đến trang web khác? Giờ đây, bạn đã có thể sử dụng tiện ích
                này thông qua một dịch vụ cung cấp thông tin mới gọi là RSS.<br />
            </p>
            <p style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                background-color: rgb(255, 255, 255); color: rgb(85, 85, 85); font-family: Arial, Tahoma;
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: 16px; orphans: 2; text-align: start; text-indent: 0px; text-transform: none;
                white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto;
                -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;">
                Có nhiều ý kiến xung quanh vấn đề giải thích từ viết tắt RSS có nghĩa gì. Tuy nhiên
                đa số đồng ý rằng đây là từ viết tắt của Really Simple Syndication- Dịch vụ cung
                cấp thông tin cực kì đơn giản. Nói ngắn gọn, dịch vụ này cho phép bạn tìm kiếm thông
                tin cần quan tâm và đăng ký để được gửi thông tin đến trực tiếp. Dịch vụ này giúp
                bạn giải quyết vấn đề về tính cập nhật của thông tin bằng việc cung cấp cho bạn
                những thông tin mới nhất mà bạn đang quan tâm.<br />
            </p>
            <p style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                background-color: rgb(255, 255, 255); color: rgb(85, 85, 85); font-family: Arial, Tahoma;
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: 16px; orphans: 2; text-align: start; text-indent: 0px; text-transform: none;
                white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto;
                -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;">
                Hiện tại không phải bất cứ trang web nào cũng cung cấp RSS, nhưng dịch vụ này sẽ
                dần trở nên phổ biến. Nhiều trang web tin tức như BBC, CNN, và New York Times đang
                cung cấp RSS.</p>
            <p style="outline-width: 0px; outline-style: none; outline-color: invert; font-size: 12px;
                color: rgb(85, 85, 85); font-family: Arial, Tahoma; font-style: normal; font-variant: normal;
                font-weight: normal; letter-spacing: normal; line-height: 16px; orphans: 2; text-align: start;
                text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px;
                -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; border-style: none;
                border-color: inherit; border-width: 0px; margin: 0px; padding: 0px; background-color: rgb(255, 255, 255);
                background-repeat: initial initial">
                &nbsp;</p>
            <div>
                <asp:Literal ID="lrTreeView" runat="server"></asp:Literal>
            </div>
            <p style="outline-width: 0px; outline-style: none; outline-color: invert; font-size: 12px;
                color: rgb(85, 85, 85); font-family: Arial, Tahoma; font-style: normal; font-variant: normal;
                font-weight: normal; letter-spacing: normal; line-height: 16px; orphans: 2; text-align: start;
                text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px;
                -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; border-style: none;
                border-color: inherit; border-width: 0px; margin: 0px; padding: 0px; background-color: rgb(255, 255, 255);
                background-repeat: initial initial">
                &nbsp;</p>
            <p style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                background-color: rgb(255, 255, 255); color: rgb(85, 85, 85); font-family: Arial, Tahoma;
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: 16px; orphans: 2; text-align: start; text-indent: 0px; text-transform: none;
                white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto;
                -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;">
                <b style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                    background-color: transparent; background-position: initial initial; background-repeat: initial initial;">
                    Làm cách nào để bắt đầu sử dụng các danh mục tin RSS?</b></p>
            <p style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                background-color: rgb(255, 255, 255); color: rgb(85, 85, 85); font-family: Arial, Tahoma;
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: 16px; orphans: 2; text-align: start; text-indent: 0px; text-transform: none;
                white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto;
                -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;">
                Nhìn chung, đầu tiên bạn cần có một thứ gọi là trình đọc tin (news reader). Có rất
                nhiều kiểu trình đọc tin, một số được nhúng trực tiếp trong trình duyệt, một số
                là các ứng dụng có thể tải về từ Internet. Tất cả những công cụ này sẽ giúp bạn
                có thể xem được thông tin và đăng kí sử dụng danh mục tin của RSS.</p>
            <p style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                background-color: rgb(255, 255, 255); color: rgb(85, 85, 85); font-family: Arial, Tahoma;
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: 16px; orphans: 2; text-align: start; text-indent: 0px; text-transform: none;
                white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto;
                -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;">
                Sau khi bạn đã chọn được một trình đọc tin, tất cả những gì bạn phải làm là lựa
                chọn nội dung thông tin mà bạn cần. Ví dụ như bạn cần thông tin mới nhất về công
                nghệ thông tin, bạn có thể sử dụng nút RSS màu cam của mục<span class="Apple-converted-space">&nbsp;</span><b
                    style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                    background-color: transparent; background-position: initial initial; background-repeat: initial initial;">Đời
                    sống</b>. Có thể kéo/thả nút này vào trong trình đọc tin của bạn, hoặc cắt/dán
                Url vào chức năng thêm danh mục tin của trình đọc tin</p>
            <p style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                background-color: rgb(255, 255, 255); color: rgb(85, 85, 85); font-family: Arial, Tahoma;
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: 16px; orphans: 2; text-align: start; text-indent: 0px; text-transform: none;
                white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto;
                -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;">
                Một số trình duyệt, trong đó có Firefox, Opera và Safari, có chức năng tự động chọn
                danh mục tin RSS cho bạn. Nếu cần biết thông tin cụ thể hơn, bạn có thể xem thông
                tin trên các trang chủ của các trình duyệt đó.</p>
            <p style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                background-color: rgb(255, 255, 255); color: rgb(85, 85, 85); font-family: Arial, Tahoma;
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: 16px; orphans: 2; text-align: start; text-indent: 0px; text-transform: none;
                white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto;
                -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;">
                Có rất nhiều loại trình đọc tin khác nhau và các phiên bản được thường xuyên cập
                nhật. Mỗi loại trình đọc tin lại đòi hỏi một hệ điều hành khác nhau, do đó bạn phải
                cân nhắc về điều đó khi lựa chọn trình đọc tin.</p>
            <p style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                background-color: rgb(255, 255, 255); color: rgb(85, 85, 85); font-family: Arial, Tahoma;
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: 16px; orphans: 2; text-align: start; text-indent: 0px; text-transform: none;
                white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto;
                -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;">
                <b style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                    background-color: transparent; background-position: initial initial; background-repeat: initial initial;">
                    Các giới hạn sử dụng</b></p>
            <p style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                background-color: rgb(255, 255, 255); color: rgb(85, 85, 85); font-family: Arial, Tahoma;
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: 16px; orphans: 2; text-align: start; text-indent: 0px; text-transform: none;
                white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto;
                -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;">
                Các nguồn kênh tin được cung cấp miễn phí cho các cá nhân và các tổ chức phi lợi
                nhuận. Chúng tôi yêu cầu bạn cung cấp rõ các thông tin cần thiết khi bạn sử dụng
                các nguồn kênh tin này từ Zing. Zing có quyền yêu cầu bạn ngừng cung cấp và phân
                tán thông tin dưới dạng này ở bất kỳ thời điểm nào và với bất kỳ lý do nào.</p>
            <p style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                background-color: rgb(255, 255, 255); color: rgb(85, 85, 85); font-family: Arial, Tahoma;
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: 16px; orphans: 2; text-align: start; text-indent: 0px; text-transform: none;
                white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto;
                -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;">
                <b style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                    background-color: transparent; background-position: initial initial; background-repeat: initial initial;">
                    Phần mềm đọc RSS</b></p>
            <p style="margin: 0px; padding: 0px; border: 0px; outline: 0px; font-size: 12px;
                background-color: rgb(255, 255, 255); color: rgb(85, 85, 85); font-family: Arial, Tahoma;
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: 16px; orphans: 2; text-align: start; text-indent: 0px; text-transform: none;
                white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto;
                -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;">
                Bạn có thể sử dụng chương trình đọc RSS<span class="Apple-converted-space">&nbsp;</span><a
                    href="http://www.newzcrawler.com/" style="margin: 0px; padding: 0px; border: 0px;
                    outline: 0px; font-size: 12px; background-color: transparent; color: rgb(2, 67, 130);
                    text-decoration: none; background-position: initial initial; background-repeat: initial initial;"
                    target="_blank">Newz Crawler</a><span class="Apple-converted-space">&nbsp;</span>để
                đọc nhanh tài liệu RSS</p>
        </div>
        <div class="rating" itemprop="aggregateRating" itemscope itemtype="http://schema.org/AggregateRating">
            <span itemprop="ratingValue">100</span> out of <span itemprop="bestRating">100</span>
            based on <span itemprop="ratingCount">
                <asp:Literal ID="lbRatingCount" runat="server"></asp:Literal></span> user ratings
        </div>
        <div class="single_share">
        </div>
        <div id="fb-root">
        </div>
        <script type="text/javascript">
            (function (d, s, id) {
                var js, fjs = d.getElementsByTagName(s)[0];
                if (d.getElementById(id)) return;
                js = d.createElement(s); js.id = id;
                js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=312319252170353";
                fjs.parentNode.insertBefore(js, fjs);
            } (document, 'script', 'facebook-jssdk'));</script>
        <div data-href="<%= CurrentPage.UrlRoot %>/upnews.aspx" class="fb-comments" data-num-posts="5"
            data-width="609">
        </div>
    </div>
</div>
