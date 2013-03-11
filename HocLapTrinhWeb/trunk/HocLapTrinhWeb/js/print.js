function CallPrint(elementId) {
    var printContent = document.getElementById(elementId);
    var windowUrl = 'print';
    var uniqueName = new Date();
    var windowName = 'Print' + uniqueName.getTime();
    var printWindow = window.open(windowUrl, windowName, 'left=0,top=0,width=400,height=300');
    print.document.open();
    printWindow.document.writeln("<link rel='stylesheet' type='text/css' href='css/default/print.css'>");
    printWindow.document.write(printContent.innerHTML);
    printWindow.document.close();
    printWindow.focus(); //print
    printWindow.close(); //dong cua so sau khi in

}

function Print() {
    var popup = "toolbar=no,location=no,directories=no,menubar=no,";
    popup += "scrollbars=yes,width=700, height=600, left=100, top=25";
    var vTitle = $('.news_detail .title').html();
    var vContent = $('.news_detail').html();
    var print = window.open("Print", "In trang", popup);
    print.document.open();
    print.document.write('<html><head><title>' + vTitle + ' - Hoclaptrinhweb.vn</title><link rel="stylesheet" type="text/css" href="css/default/print.css">');
    print.document.write('</head><body>');
    print.document.write('<div id="PrintContent">');
    print.document.write('<img class="logo" src="css/default/images/logo.png" />');
    print.document.write('<div class="content"><p class="print"><a href="javascript:self.print();"  title="in trang"><img src="css/default/images/btn-print.gif" /></a></p>' + vContent + '</div>');
    print.document.write('<div class="footer"><p>Copyright © 2012. All rights reserved. <a href="http://diendandaihoc.vn">Hoclaptrinhweb.com</a>-Cộng đồng sinh viên Việt Nam</p></div>');
    print.document.write('</div></body></html>');
    print.document.close();
    print.focus();
}