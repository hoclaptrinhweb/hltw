﻿using System.Web.UI;
using System.IO;
namespace HocLapTrinhWeb.Utilities
{
    /// <summary>
    /// Lớp download file từ server
    /// </summary>
    public class Download
    {
        Page _thisPage = new Page();
        public Download(Page CurrentPage)
        {
            _thisPage = CurrentPage;
        }
        /// <summary>
        /// Download file từ server
        /// </summary>
        /// <param name="FileDownloadPath">Đường dẫn file trên server</param>
        /// <param name="ContentType">Loại file</param>
        public void DownloadFile(string FileDownloadPath, EnumDownload.ContentType ContentType)
        {
            FileInfo _FileInfo = new FileInfo(FileDownloadPath);
            _thisPage.Response.ContentType = GetContentType(ContentType);
            _thisPage.Response.AppendHeader("Content-Disposition", "attachment; filename=" + _FileInfo.Name + "");
            _thisPage.Response.TransmitFile(FileDownloadPath);
            _thisPage.Response.End();

        }

        /// <summary>
        /// Download file từ server
        /// </summary>
        /// <param name="FileDownloadPath">Đường dẫn file trên server</param>
        /// <param name="ContentType">Loại file</param>
        public void DownloadFile(string FileDownloadPath, string ContentType)
        {

            FileInfo _FileInfo = new FileInfo(FileDownloadPath);
            _thisPage.Response.ContentType = ContentType;
            _thisPage.Response.AppendHeader("Content-Disposition", "attachment; filename=" + _FileInfo.Name + "");
            _thisPage.Response.TransmitFile(FileDownloadPath);
            _thisPage.Response.End();

        }

        /// <summary>
        /// Download file từ server
        /// </summary>
        /// <param name="FileDownloadPath">Đường dẫn file trên server</param>
        /// <param name="FileName">Tên file lưu về client</param>
        /// <param name="ContentType">Loại file</param>
        public void DownloadFile(string FileDownloadPath, string FileName, EnumDownload.ContentType ContentType)
        {

            _thisPage.Response.ContentType = GetContentType(ContentType);
            _thisPage.Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "");
            _thisPage.Response.TransmitFile(FileDownloadPath);
            _thisPage.Response.End();

        }

        /// <summary>
        /// Download file từ server
        /// </summary>
        /// <param name="FileDownloadPath">Đường dẫn file trên server</param>
        /// <param name="FileName">Tên file lưu về client</param>
        /// <param name="ContentType">Loại file</param>
        public void DownloadFile(string FileDownloadPath, string FileName, string ContentType)
        {

            _thisPage.Response.ContentType = ContentType;
            _thisPage.Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "");
            _thisPage.Response.TransmitFile(FileDownloadPath);
            _thisPage.Response.End();

        }


        /// <summary>
        /// Hàm lấy lên enum
        /// </summary>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        private string GetContentType(EnumDownload.ContentType ContentType)
        {
            switch (ContentType.GetHashCode().ToString())
            {
                case "0":
                    return "application/octet-stream";
                    break;
                case "1":
                    return "application/pdf";
                    break;
                case "2":
                    return "application/msword";
                    break;
                case "3":
                    return "application/vnd.ms-excel";
                    break;
                case "4":
                    return "application/vnd.ms-powerpoint";
                    break;
                case "5":
                    return "application/zip";
                    break;
                case "6":
                    return "application/x-shockwave-flash";
                    break;
                case "7":
                    return "audio/mpeg";
                    break;
                case "8":
                    return "audio/x-wav";
                    break;
                case "9":
                    return "application/x-chm";
                    break;

                default:
                    return "application/octet-stream";
                    break;
            }
        }

        /// <summary>
        /// Danh sách content type
        /// </summary>
        public string ListOfMimeTypes = @"application/andrew-inset 	ez
application/mac-binhex40 	hqx
application/mac-compactpro 	cpt
application/mathml+xml 	mathml
application/msword 	doc
application/octet-stream 	bin dms lha lzh exe class so dll (all format)
application/oda 	oda
application/ogg 	ogg
application/pdf 	pdf
application/postscript 	ai eps ps
application/rdf+xml 	rdf
application/smil 	smi smil
application/srgs 	gram
application/srgs+xml 	grxml
application/vnd.mif 	mif
application/vnd.mozilla.xul+xml 	xul
application/vnd.ms-excel 	xls
application/vnd.ms-powerpoint 	ppt
application/vnd.wap.wbxml 	wbxml
application/vnd.wap.wmlc 	.wmlc wmlc
application/vnd.wap.wmlscriptc 	.wmlsc wmlsc
application/voicexml+xml 	vxml
application/x-bcpio 	bcpio
application/x-cdlink 	vcd
application/x-chess-pgn 	pgn
application/x-cpio 	cpio
application/x-csh 	csh
application/x-director 	dcr dir dxr
application/x-dvi 	dvi
application/x-futuresplash 	spl
application/x-gtar 	gtar
application/x-hdf 	hdf
application/x-httpd-php 	.php .php4 .php3 .phtml
application/x-httpd-php-source 	.phps
application/x-javascript 	js
application/x-koan 	skp skd skt skm
application/x-latex 	latex
application/x-netcdf 	nc cdf
application/x-pkcs7-crl 	.crl
application/x-sh 	sh
application/x-shar 	shar
application/x-shockwave-flash 	swf
application/x-stuffit 	sit
application/x-sv4cpio 	sv4cpio
application/x-sv4crc 	sv4crc
application/x-tar 	.tgz tar
application/x-tcl 	tcl
application/x-tex 	tex
application/x-texinfo 	texinfo texi
application/x-troff 	t tr roff
application/x-troff-man 	man
application/x-troff-me 	me
application/x-troff-ms 	ms
application/x-ustar 	ustar
application/x-wais-source 	src
application/x-x509-ca-cert 	.crt
application/xhtml+xml 	xhtml xht
application/xml 	xml xsl
application/xml-dtd 	dtd
application/xslt+xml 	xslt
application/zip 	zip
audio/basic 	au snd
audio/midi 	mid midi kar
audio/mpeg 	mpga mp2 mp3
audio/x-aiff 	aif aiff aifc
audio/x-mpegurl 	m3u
audio/x-pn-realaudio 	ram rm
audio/x-pn-realaudio-plugin 	rpm
audio/x-realaudio 	ra
audio/x-wav 	wav
chemical/x-pdb 	pdb
chemical/x-xyz 	xyz
image/bmp 	bmp
image/cgm 	cgm
image/gif 	gif
image/ief 	ief
image/jpeg 	jpeg jpg jpe
image/png 	png
image/svg+xml 	svg
image/tiff 	tiff tif
image/vnd.djvu 	djvu djv
image/vnd.wap.wbmp 	.wbmp wbmp
image/x-cmu-raster 	ras
image/x-icon 	ico
image/x-portable-anymap 	pnm
image/x-portable-bitmap 	pbm
image/x-portable-graymap 	pgm
image/x-portable-pixmap 	ppm
image/x-rgb 	rgb
image/x-xbitmap 	xbm
image/x-xpixmap 	xpm
image/x-xwindowdump 	xwd
model/iges 	igs iges
model/mesh 	msh mesh silo
model/vrml 	wrl vrml
text/calendar 	ics ifb
text/css 	css
text/html 	.shtml html htm
text/plain 	asc txt
text/richtext 	rtx
text/rtf 	rtf
text/sgml 	sgml sgm
text/tab-separated-values 	tsv
text/vnd.wap.wml 	.wml wml
text/vnd.wap.wmlscript 	.wmls wmls
text/x-setext 	etx
video/mpeg 	mpeg mpg mpe
video/quicktime 	qt mov
video/vnd.mpegurl 	mxu
video/x-msvideo 	avi
video/x-sgi-movie 	movie
x-conference/x-cooltalk    ice

--------------
application/activemessage
application/andrew-inset			ez
application/applefile
application/atomicmail
application/batch-SMTP
application/beep+xml
application/cals-1840
application/commonground
application/cu-seeme				csm cu
application/cybercash
application/dca-rft
application/dec-dx
application/docbook+xml
application/dsptype				tsp
application/dvcs
application/edi-consent
application/edifact
application/edi-x12
application/eshop
application/font-tdpfr
application/futuresplash			spl
application/ghostview
application/hta					hta
application/http
application/hyperstudio
application/iges
application/index
application/index.cmd
application/index.obj
application/index.response
application/index.vnd
application/iotp
application/ipp
application/isup
application/mac-compactpro			cpt
application/marc
application/mac-binhex40			hqx
application/macwriteii
application/mathematica				nb
application/mathematica-old
application/msaccess				mdb
application/msword				doc dot
application/news-message-id
application/news-transmission
application/octet-stream			bin
application/ocsp-request
application/ocsp-response
application/oda					oda
application/ogg					ogg
application/parityfec
application/pics-rules				prf
application/pgp-encrypted
application/pgp-keys				key
application/pdf					pdf
application/pgp-signature			pgp
application/pkcs10
application/pkcs7-mime
application/pkcs7-signature
application/pkix-cert
application/pkixcmp
application/pkix-crl
application/postscript				ps ai eps
application/prs.alvestrand.titrax-sheet
application/prs.cww
application/prs.nprend
application/qsig
application/rar					rar
application/rdf+xml				rdf
application/remote-printing
application/riscos
application/rss+xml				rss
application/rtf					rtf
application/sdp
application/set-payment
application/set-payment-initiation
application/set-registration
application/set-registration-initiation
application/sgml
application/sgml-open-catalog
application/sieve
application/slate
application/smil				smi smil
application/timestamp-query
application/timestamp-reply
application/vemmi
application/whoispp-query
application/whoispp-response
application/wita
application/wordperfect5.1			wp5
application/x400-bp
application/xhtml+xml				xht xhtml
application/xml					xml xsl
application/xml-dtd
application/xml-external-parsed-entity
application/zip					zip
application/vnd.3M.Post-it-Notes
application/vnd.accpac.simply.aso
application/vnd.accpac.simply.imp
application/vnd.acucobol
application/vnd.aether.imp
application/vnd.anser-web-certificate-issue-initiation
application/vnd.anser-web-funds-transfer-initiation
application/vnd.audiograph
application/vnd.bmi
application/vnd.businessobjects
application/vnd.canon-cpdl
application/vnd.canon-lips
application/vnd.cinderella			cdy
application/vnd.claymore
application/vnd.commerce-battelle
application/vnd.commonspace
application/vnd.comsocaller
application/vnd.contact.cmsg
application/vnd.cosmocaller
application/vnd.ctc-posml
application/vnd.cups-postscript
application/vnd.cups-raster
application/vnd.cups-raw
application/vnd.cybank
application/vnd.dna
application/vnd.dpgraph
application/vnd.dxr
application/vnd.ecdis-update
application/vnd.ecowin.chart
application/vnd.ecowin.filerequest
application/vnd.ecowin.fileupdate
application/vnd.ecowin.series
application/vnd.ecowin.seriesrequest
application/vnd.ecowin.seriesupdate
application/vnd.enliven
application/vnd.epson.esf
application/vnd.epson.msf
application/vnd.epson.quickanime
application/vnd.epson.salt
application/vnd.epson.ssf
application/vnd.ericsson.quickcall
application/vnd.eudora.data
application/vnd.fdf
application/vnd.ffsns
application/vnd.flographit
application/vnd.framemaker
application/vnd.fsc.weblaunch
application/vnd.fujitsu.oasys
application/vnd.fujitsu.oasys2
application/vnd.fujitsu.oasys3
application/vnd.fujitsu.oasysgp
application/vnd.fujitsu.oasysprs
application/vnd.fujixerox.ddd
application/vnd.fujixerox.docuworks
application/vnd.fujixerox.docuworks.binder
application/vnd.fut-misnet
application/vnd.grafeq
application/vnd.groove-account
application/vnd.groove-identity-message
application/vnd.groove-injector
application/vnd.groove-tool-message
application/vnd.groove-tool-template
application/vnd.groove-vcard
application/vnd.hhe.lesson-player
application/vnd.hp-HPGL
application/vnd.hp-PCL
application/vnd.hp-PCLXL
application/vnd.hp-hpid
application/vnd.hp-hps
application/vnd.httphone
application/vnd.hzn-3d-crossword
application/vnd.ibm.MiniPay
application/vnd.ibm.afplinedata
application/vnd.ibm.modcap
application/vnd.informix-visionary
application/vnd.intercon.formnet
application/vnd.intertrust.digibox
application/vnd.intertrust.nncp
application/vnd.intu.qbo
application/vnd.intu.qfx
application/vnd.irepository.package+xml
application/vnd.is-xpr
application/vnd.japannet-directory-service
application/vnd.japannet-jpnstore-wakeup
application/vnd.japannet-payment-wakeup
application/vnd.japannet-registration
application/vnd.japannet-registration-wakeup
application/vnd.japannet-setstore-wakeup
application/vnd.japannet-verification
application/vnd.japannet-verification-wakeup
application/vnd.koan
application/vnd.lotus-1-2-3
application/vnd.lotus-approach
application/vnd.lotus-freelance
application/vnd.lotus-notes
application/vnd.lotus-organizer
application/vnd.lotus-screencam
application/vnd.lotus-wordpro
application/vnd.mcd
application/vnd.mediastation.cdkey
application/vnd.meridian-slingshot
application/vnd.mif	      mif
application/vnd.minisoft-hp3000-save
application/vnd.mitsubishi.misty-guard.trustweb
application/vnd.mobius.daf
application/vnd.mobius.dis
application/vnd.mobius.msl
application/vnd.mobius.plc
application/vnd.mobius.txf
application/vnd.motorola.flexsuite
application/vnd.motorola.flexsuite.adsi
application/vnd.motorola.flexsuite.fis
application/vnd.motorola.flexsuite.gotap
application/vnd.motorola.flexsuite.kmr
application/vnd.motorola.flexsuite.ttc
application/vnd.motorola.flexsuite.wem
application/vnd.mozilla.xul+xml			xul
application/vnd.ms-artgalry
application/vnd.ms-asf
application/vnd.ms-excel			xls xlb xlt
application/vnd.ms-lrm
application/vnd.ms-pki.seccat			cat
application/vnd.ms-pki.stl			stl
application/vnd.ms-powerpoint			ppt pps
application/vnd.ms-project
application/vnd.ms-tnef
application/vnd.ms-works
application/vnd.mseq
application/vnd.msign
application/vnd.music-niff
application/vnd.musician
application/vnd.netfpx
application/vnd.noblenet-directory
application/vnd.noblenet-sealer
application/vnd.noblenet-web
application/vnd.novadigm.EDM
application/vnd.novadigm.EDX
application/vnd.novadigm.EXT
application/vnd.osa.netdeploy
application/vnd.palm
application/vnd.pg.format
application/vnd.pg.osasli
application/vnd.powerbuilder6
application/vnd.powerbuilder6-s
application/vnd.powerbuilder7
application/vnd.powerbuilder7-s
application/vnd.powerbuilder75
application/vnd.powerbuilder75-s
application/vnd.previewsystems.box
application/vnd.publishare-delta-tree
application/vnd.pvi.ptid1
application/vnd.pwg-xhtml-print+xml
application/vnd.rapid
application/vnd.s3sms
application/vnd.seemail
application/vnd.shana.informed.formdata
application/vnd.shana.informed.formtemplate
application/vnd.shana.informed.interchange
application/vnd.shana.informed.package
application/vnd.smaf				mmf
application/vnd.sss-cod
application/vnd.sss-dtf
application/vnd.sss-ntf
application/vnd.stardivision.calc		sdc
application/vnd.stardivision.draw		sda
application/vnd.stardivision.impress		sdd sdp
application/vnd.stardivision.math		smf
application/vnd.stardivision.writer		sdw vor
application/vnd.stardivision.writer-global	sgl
application/vnd.street-stream
application/vnd.sun.xml.calc			sxc
application/vnd.sun.xml.calc.template		stc
application/vnd.sun.xml.draw			sxd
application/vnd.sun.xml.draw.template		std
application/vnd.sun.xml.impress			sxi
application/vnd.sun.xml.impress.template	sti
application/vnd.sun.xml.math			sxm
application/vnd.sun.xml.writer			sxw
application/vnd.sun.xml.writer.global		sxg
application/vnd.sun.xml.writer.template		stw
application/vnd.svd
application/vnd.swiftview-ics
application/vnd.symbian.install			sis
application/vnd.triscape.mxs
application/vnd.trueapp
application/vnd.truedoc
application/vnd.tve-trigger
application/vnd.ufdl
application/vnd.uplanet.alert
application/vnd.uplanet.alert-wbxml
application/vnd.uplanet.bearer-choice
application/vnd.uplanet.bearer-choice-wbxml
application/vnd.uplanet.cacheop
application/vnd.uplanet.cacheop-wbxml
application/vnd.uplanet.channel
application/vnd.uplanet.channel-wbxml
application/vnd.uplanet.list
application/vnd.uplanet.list-wbxml
application/vnd.uplanet.listcmd
application/vnd.uplanet.listcmd-wbxml
application/vnd.uplanet.signal
application/vnd.vcx
application/vnd.vectorworks
application/vnd.vidsoft.vidconference
application/vnd.visio				vsd
application/vnd.vividence.scriptfile
application/vnd.wap.sic
application/vnd.wap.slc
application/vnd.wap.wbxml			wbxml
application/vnd.wap.wmlc			wmlc
application/vnd.wap.wmlscriptc			wmlsc
application/vnd.webturbo
application/vnd.wrq-hp3000-labelled
application/vnd.wt.stf
application/vnd.xara
application/vnd.xfdl
application/vnd.yellowriver-custom-menu
application/x-123				wk
application/x-apple-diskimage			dmg
application/x-bcpio				bcpio
application/x-bittorrent			torrent
application/x-cdf				cdf
application/x-cdlink				vcd
application/x-chess-pgn				pgn
application/x-chm				chm
application/x-core
application/x-cpio				cpio
application/x-csh				csh
application/x-debian-package			deb
application/x-director				dcr dir dxr
application/x-doom				wad
application/x-dms				dms
application/x-dvi				dvi
application/x-executable
application/x-flac				flac
application/x-font				pfa pfb gsf pcf pcf.Z
application/x-futuresplash			spl
application/x-gnumeric				gnumeric
application/x-go-sgf				sgf
application/x-graphing-calculator		gcf
application/x-gtar				gtar tgz taz
application/x-hdf				hdf
application/x-httpd-php				phtml pht php
application/x-httpd-php-source			phps
application/x-httpd-php3			php3
application/x-httpd-php3-preprocessed		php3p
application/x-httpd-php4			php4
application/x-ica				ica
application/x-internet-signup			ins isp
application/x-iphone				iii
application/x-java-applet
application/x-java-archive			jar
application/x-java-bean
application/x-java-jnlp-file			jnlp
application/x-java-serialized-object		ser
application/x-java-vm				class
application/x-javascript			js
application/x-kdelnk
application/x-kchart				chrt
application/x-killustrator			kil
application/x-kpresenter			kpr kpt
application/x-koan				skp skd skt skm
application/x-kspread				ksp
application/x-kword				kwd kwt
application/x-latex				latex
application/x-lha				lha
application/x-lzh				lzh
application/x-lzx				lzx
application/x-maker				frm maker frame fm fb book fbdoc
application/x-mif				mif
application/x-ms-wmz				wmz
application/x-ms-wmd				wmd
application/x-msdos-program			com exe bat dll
application/x-msi				msi
application/x-netcdf				nc
application/x-ns-proxy-autoconfig		pac
application/x-nwc				nwc
application/x-object				o
application/x-oz-application			oza
application/x-pkcs7-certreqresp			p7r
application/x-pkcs7-crl				crl
application/x-python-code			pyc pyo
application/x-quicktimeplayer			qtl
application/x-redhat-package-manager		rpm
application/x-rx
application/x-sh
application/x-shar				shar
application/x-shellscript
application/x-shockwave-flash			swf swfl
application/x-sh				sh
application/x-stuffit				sit
application/x-sv4cpio				sv4cpio
application/x-sv4crc				sv4crc
application/x-tar				tar
application/x-tcl				tcl
application/x-tex-gf				gf
application/x-tex-pk				pk
application/x-texinfo				texinfo texi
application/x-trash				~ % bak old sik
application/x-troff				t tr roff
application/x-troff-man				man
application/x-troff-me				me
application/x-troff-ms				ms
application/x-ustar				ustar
application/x-videolan
application/x-wais-source			src
application/x-wingz				wz
application/x-x509-ca-cert			crt
application/x-xcf				xcf
application/x-xfig				fig

audio/32kadpcm
audio/basic					au snd
audio/g.722.1
audio/l16
audio/midi					mid midi kar
audio/mp4a-latm
audio/mpa-robust
audio/mpeg					mpga mpega mp2 mp3 m4a
audio/mpegurl					m3u
audio/parityfec
audio/prs.sid					sid
audio/telephone-event
audio/tone
audio/vnd.cisco.nse
audio/vnd.cns.anp1
audio/vnd.cns.inf1
audio/vnd.digital-winds
audio/vnd.everad.plj
audio/vnd.lucent.voice
audio/vnd.nortel.vbk
audio/vnd.nuera.ecelp4800
audio/vnd.nuera.ecelp7470
audio/vnd.nuera.ecelp9600
audio/vnd.octel.sbc
audio/vnd.qcelp
audio/vnd.rhetorex.32kadpcm
audio/vnd.vmx.cvsd
audio/x-aiff					aif aiff aifc
audio/x-gsm					gsm
audio/x-mpegurl					m3u
audio/x-ms-wma					wma
audio/x-ms-wax					wax
audio/x-pn-realaudio-plugin
audio/x-pn-realaudio				ra rm ram
audio/x-realaudio				ra
audio/x-scpls					pls
audio/x-sd2					sd2
audio/x-wav					wav

chemical/x-pdb					pdb
chemical/x-xyz					xyz

image/cgm
image/g3fax
image/gif					gif
image/ief					ief
image/jpeg					jpeg jpg jpe
image/naplps
image/pcx					pcx
image/png					png
image/prs.btif
image/prs.pti
image/svg+xml					svg svgz
image/tiff					tiff tif
image/vnd.cns.inf2
image/vnd.djvu					djvu djv
image/vnd.dwg
image/vnd.dxf
image/vnd.fastbidsheet
image/vnd.fpx
image/vnd.fst
image/vnd.fujixerox.edmics-mmr
image/vnd.fujixerox.edmics-rlc
image/vnd.mix
image/vnd.net-fpx
image/vnd.svf
image/vnd.wap.wbmp				wbmp
image/vnd.xiff
image/x-cmu-raster				ras
image/x-coreldraw				cdr
image/x-coreldrawpattern			pat
image/x-coreldrawtemplate			cdt
image/x-corelphotopaint				cpt
image/x-icon					ico
image/x-jg					art
image/x-jng					jng
image/x-ms-bmp					bmp
image/x-photoshop				psd
image/x-portable-anymap				pnm
image/x-portable-bitmap				pbm
image/x-portable-graymap			pgm
image/x-portable-pixmap				ppm
image/x-rgb					rgb
image/x-xbitmap					xbm
image/x-xpixmap					xpm
image/x-xwindowdump				xwd

inode/chardevice
inode/blockdevice
inode/directory-locked
inode/directory
inode/fifo
inode/socket

message/delivery-status
message/disposition-notification
message/external-body
message/http
message/s-http
message/news
message/partial
message/rfc822

model/iges					igs iges
model/mesh					msh mesh silo
model/vnd.dwf
model/vnd.flatland.3dml
model/vnd.gdl
model/vnd.gs-gdl
model/vnd.gtw
model/vnd.mts
model/vnd.vtu
model/vrml					wrl vrml

multipart/alternative
multipart/appledouble
multipart/byteranges
multipart/digest
multipart/encrypted
multipart/form-data
multipart/header-set
multipart/mixed
multipart/parallel
multipart/related
multipart/report
multipart/signed
multipart/voice-message

text/calendar					ics icz
text/comma-separated-values			csv
text/css					css
text/directory
text/english
text/enriched
text/h323					323
text/html					htm html shtml
text/iuls					uls
text/mathml					mml
text/parityfec
text/plain					asc txt text diff pot
text/prs.lines.tag
text/rfc822-headers
text/richtext					rtx
text/rtf					rtf
text/scriptlet					sct wsc
text/t140
text/texmacs					tm ts
text/tab-separated-values			tsv
text/uri-list
text/vnd.abc
text/vnd.curl
text/vnd.DMClientScript
text/vnd.flatland.3dml
text/vnd.fly
text/vnd.fmi.flexstor
text/vnd.in3d.3dml
text/vnd.in3d.spot
text/vnd.IPTC.NewsML
text/vnd.IPTC.NITF
text/vnd.latex-z
text/vnd.motorola.reflex
text/vnd.ms-mediapackage
text/vnd.sun.j2me.app-descriptor		jad
text/vnd.wap.si
text/vnd.wap.sl
text/vnd.wap.wml				wml
text/vnd.wap.wmlscript				wmls
text/x-c++hdr					h++ hpp hxx hh
text/x-c++src					c++ cpp cxx cc
text/x-chdr					h
text/x-crontab
text/x-csh					csh
text/x-csrc					c
text/x-java					java
text/x-makefile
text/x-moc					moc
text/x-pascal					p pas
text/x-pcs-gcd					gcd
text/x-perl					pl pm
text/x-python					py
text/x-server-parsed-html
text/x-setext					etx
text/x-sh					sh
text/x-tcl					tcl tk
text/x-tex					tex ltx sty cls
text/x-vcalendar				vcs
text/x-vcard					vcf

video/dl					dl
video/fli					fli
video/gl					gl
video/mpeg					mpeg mpg mpe
video/mp4					mp4
video/quicktime					qt mov
video/mp4v-es
video/parityfec
video/pointer
video/vnd.fvt
video/vnd.motorola.video
video/vnd.motorola.videop
video/vnd.mpegurl				mxu
video/vnd.mts
video/vnd.nokia.interleaved-multimedia
video/vnd.vivo
video/x-dv					dif dv
video/x-la-asf					lsf lsx
video/x-mng					mng
video/x-ms-asf					asf asx
video/x-ms-wm					wm
video/x-ms-wmv					wmv
video/x-ms-wmx					wmx
video/x-ms-wvx					wvx
video/x-msvideo					avi
video/x-sgi-movie				movie

x-conference/x-cooltalk				ice

x-world/x-vrml					vrm vrml wrl


";
    }


    //public class EnumDownload
    //{
    //    public enum ContentType
    //    {
    //        octetStream = 0,
    //        pdf = 1,
    //        word = 2,
    //        excel = 3,
    //        powerpoint = 4,
    //        zip = 5,
    //        swf = 6,
    //        mp3 = 7,
    //        wav = 8,
    //    }
    //}
}

public class EnumDownload
{
    public enum ContentType
    {
        octetStream = 0,
        pdf = 1,
        word = 2,
        excel = 3,
        powerpoint = 4,
        zip = 5,
        swf = 6,
        mp3 = 7,
        wav = 8,
        chm=9,
    }

}

/*
List of mine types
application/andrew-inset 	ez
application/mac-binhex40 	hqx
application/mac-compactpro 	cpt
application/mathml+xml 	mathml
application/msword 	doc
application/octet-stream 	bin dms lha lzh exe class so dll (all format)
application/oda 	oda
application/ogg 	ogg
application/pdf 	pdf
application/postscript 	ai eps ps
application/rdf+xml 	rdf
application/smil 	smi smil
application/srgs 	gram
application/srgs+xml 	grxml
application/vnd.mif 	mif
application/vnd.mozilla.xul+xml 	xul
application/vnd.ms-excel 	xls
application/vnd.ms-powerpoint 	ppt
application/vnd.wap.wbxml 	wbxml
application/vnd.wap.wmlc 	.wmlc wmlc
application/vnd.wap.wmlscriptc 	.wmlsc wmlsc
application/voicexml+xml 	vxml
application/x-bcpio 	bcpio
application/x-cdlink 	vcd
application/x-chess-pgn 	pgn
application/x-cpio 	cpio
application/x-csh 	csh
application/x-director 	dcr dir dxr
application/x-dvi 	dvi
application/x-futuresplash 	spl
application/x-gtar 	gtar
application/x-hdf 	hdf
application/x-httpd-php 	.php .php4 .php3 .phtml
application/x-httpd-php-source 	.phps
application/x-javascript 	js
application/x-koan 	skp skd skt skm
application/x-latex 	latex
application/x-netcdf 	nc cdf
application/x-pkcs7-crl 	.crl
application/x-sh 	sh
application/x-shar 	shar
application/x-shockwave-flash 	swf
application/x-stuffit 	sit
application/x-sv4cpio 	sv4cpio
application/x-sv4crc 	sv4crc
application/x-tar 	.tgz tar
application/x-tcl 	tcl
application/x-tex 	tex
application/x-texinfo 	texinfo texi
application/x-troff 	t tr roff
application/x-troff-man 	man
application/x-troff-me 	me
application/x-troff-ms 	ms
application/x-ustar 	ustar
application/x-wais-source 	src
application/x-x509-ca-cert 	.crt
application/xhtml+xml 	xhtml xht
application/xml 	xml xsl
application/xml-dtd 	dtd
application/xslt+xml 	xslt
application/zip 	zip
audio/basic 	au snd
audio/midi 	mid midi kar
audio/mpeg 	mpga mp2 mp3
audio/x-aiff 	aif aiff aifc
audio/x-mpegurl 	m3u
audio/x-pn-realaudio 	ram rm
audio/x-pn-realaudio-plugin 	rpm
audio/x-realaudio 	ra
audio/x-wav 	wav
chemical/x-pdb 	pdb
chemical/x-xyz 	xyz
image/bmp 	bmp
image/cgm 	cgm
image/gif 	gif
image/ief 	ief
image/jpeg 	jpeg jpg jpe
image/png 	png
image/svg+xml 	svg
image/tiff 	tiff tif
image/vnd.djvu 	djvu djv
image/vnd.wap.wbmp 	.wbmp wbmp
image/x-cmu-raster 	ras
image/x-icon 	ico
image/x-portable-anymap 	pnm
image/x-portable-bitmap 	pbm
image/x-portable-graymap 	pgm
image/x-portable-pixmap 	ppm
image/x-rgb 	rgb
image/x-xbitmap 	xbm
image/x-xpixmap 	xpm
image/x-xwindowdump 	xwd
model/iges 	igs iges
model/mesh 	msh mesh silo
model/vrml 	wrl vrml
text/calendar 	ics ifb
text/css 	css
text/html 	.shtml html htm
text/plain 	asc txt
text/richtext 	rtx
text/rtf 	rtf
text/sgml 	sgml sgm
text/tab-separated-values 	tsv
text/vnd.wap.wml 	.wml wml
text/vnd.wap.wmlscript 	.wmls wmls
text/x-setext 	etx
video/mpeg 	mpeg mpg mpe
video/quicktime 	qt mov
video/vnd.mpegurl 	mxu
video/x-msvideo 	avi
video/x-sgi-movie 	movie
x-conference/x-cooltalk 	ice
 
 */