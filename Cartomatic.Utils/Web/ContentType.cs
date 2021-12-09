using System;
using System.Collections.Generic;
using System.Text;

namespace Cartomatic.Utils.Web
{
    public static class ContentType
    {
        public const string CONTENT_UNKNOWN = "unknown";
        public const string CONTENT_BINARY = "application/octet-stream";


        /// <summary>
        /// Gets content type info for a file extension or mime
        /// </summary>
        /// <param name="extensionOrMime"></param>
        /// <returns></returns>
        /// <remarks>borrowed from https://www.freeformatter.com/mime-types-list.html</remarks>
        public static (string name, string contentType, string extension, string description, bool contentDisposition) GetContentTypeInfo(string extensionOrMime)
        {
            //remove dots from extension if any
            extensionOrMime = extensionOrMime.Replace(".", "");

            //Here you will have a case for each position you use
            switch (extensionOrMime.ToLower())
            {
                default:
                    return (CONTENT_UNKNOWN, CONTENT_UNKNOWN, CONTENT_UNKNOWN, CONTENT_UNKNOWN, false);

                case "x3d":
                case "application/vnd.hzn-3d-crossword":
                    return (" ", "application/vnd.hzn-3d-crossword", ".x3d", "IANA: 3D Crossword Plugin", true);
                case "3gp":
                case "video/3gpp": return ("3GP", "video/3gpp", ".3gp", "Wikipedia: 3GP", true);
                case "3g2":
                case "video/3gpp2": return ("3GP2", "video/3gpp2", ".3g2", "Wikipedia: 3G2", true);
                case "mseq":
                case "application/vnd.mseq":
                    return ("3GPP MSEQ File", "application/vnd.mseq", ".mseq", "IANA: 3GPP MSEQ File", true);
                case "pwn":
                case "application/vnd.3m.post-it-notes":
                    return ("3M Post It Notes", "application/vnd.3m.post-it-notes", ".pwn", "IANA: 3M Post It Notes",
                        true);
                case "plb":
                case "application/vnd.3gpp.pic-bw-large":
                    return ("3rd Generation Partnership Project - Pic Large", "application/vnd.3gpp.pic-bw-large",
                        ".plb", "3GPP", true);
                case "psb":
                case "application/vnd.3gpp.pic-bw-small":
                    return ("3rd Generation Partnership Project - Pic Small", "application/vnd.3gpp.pic-bw-small",
                        ".psb", "3GPP", true);
                case "pvb":
                case "application/vnd.3gpp.pic-bw-var":
                    return ("3rd Generation Partnership Project - Pic Var", "application/vnd.3gpp.pic-bw-var", ".pvb",
                        "3GPP", true);
                case "tcap":
                case "application/vnd.3gpp2.tcap":
                    return ("3rd Generation Partnership Project - Transaction Capabilities Application Part",
                        "application/vnd.3gpp2.tcap", ".tcap", "3GPP", true);
                case "7z":
                case "application/x-7z-compressed":
                    return ("7-Zip", "application/x-7z-compressed", ".7z", "Wikipedia: 7-Zip", true);
                case "abw":
                case "application/x-abiword":
                    return ("AbiWord", "application/x-abiword", ".abw", "Wikipedia: AbiWord", true);
                case "ace":
                case "application/x-ace-compressed":
                    return ("Ace Archive", "application/x-ace-compressed", ".ace", "Wikipedia: ACE", true);
                case "acc":
                case "application/vnd.americandynamics.acc":
                    return ("Active Content Compression", "application/vnd.americandynamics.acc", ".acc",
                        "IANA: Active Content Compression", true);
                case "acu":
                case "application/vnd.acucobol":
                    return ("ACU Cobol", "application/vnd.acucobol", ".acu", "IANA: ACU Cobol", true);
                case "atc":
                case "application/vnd.acucorp":
                    return ("ACU Cobol", "application/vnd.acucorp", ".atc", "IANA: ACU Cobol", true);
                case "adp":
                case "audio/adpcm":
                    return ("Adaptive differential pulse-code modulation", "audio/adpcm", ".adp", "Wikipedia: ADPCM",
                        true);
                case "aab":
                case "application/x-authorware-bin":
                    return ("Adobe (Macropedia); Authorware - Binary File", "application/x-authorware-bin", ".aab",
                        "Wikipedia: Authorware", true);
                case "aam":
                case "application/x-authorware-map":
                    return ("Adobe (Macropedia); Authorware - Map", "application/x-authorware-map", ".aam",
                        "Wikipedia: Authorware", true);
                case "aas":
                case "application/x-authorware-seg":
                    return ("Adobe (Macropedia); Authorware - Segment File", "application/x-authorware-seg", ".aas",
                        "Wikipedia: Authorware", true);
                case "air":
                case "application/vnd.adobe.air-application-installer-package+zip":
                    return ("Adobe AIR Application", "application/vnd.adobe.air-application-installer-package+zip",
                        ".air", "Building AIR Applications", true);
                case "swf":
                case "application/x-shockwave-flash":
                    return ("Adobe Flash", "application/x-shockwave-flash", ".swf", "Wikipedia: Adobe Flash", true);
                case "fxp":
                case "application/vnd.adobe.fxp":
                    return ("Adobe Flex Project", "application/vnd.adobe.fxp", ".fxp", "IANA: Adobe Flex Project",
                        true);
                case "pdf":
                case "application/pdf":
                    return ("Adobe Portable Document Format", "application/pdf", ".pdf", "Adobe PDF", true);
                case "ppd":
                case "application/vnd.cups-ppd":
                    return ("Adobe PostScript Printer Description File Format", "application/vnd.cups-ppd", ".ppd",
                        "IANA: Cups", true);
                case "dir":
                case "application/x-director":
                    return ("Adobe Shockwave Player", "application/x-director", ".dir",
                        "Wikipedia: Adobe Shockwave Player", true);
                case "xdp":
                case "application/vnd.adobe.xdp+xml":
                    return ("Adobe XML Data Package", "application/vnd.adobe.xdp+xml", ".xdp",
                        "Wikipedia: XML Data Package", true);
                case "xfdf":
                case "application/vnd.adobe.xfdf":
                    return ("Adobe XML Forms Data Format", "application/vnd.adobe.xfdf", ".xfdf",
                        "Wikipedia: XML Portable Document Format", true);
                case "aac":
                case "audio/x-aac":
                    return ("Advanced Audio Coding (AAC);", "audio/x-aac", ".aac", "Wikipedia: AAC", true);
                case "ahead":
                case "application/vnd.ahead.space":
                    return ("Ahead AIR Application", "application/vnd.ahead.space", ".ahead",
                        "IANA: Ahead AIR Application", true);
                case "azf":
                case "application/vnd.airzip.filesecure.azf":
                    return ("AirZip FileSECURE", "application/vnd.airzip.filesecure.azf", ".azf", "IANA: AirZip", true);
                case "azs":
                case "application/vnd.airzip.filesecure.azs":
                    return ("AirZip FileSECURE", "application/vnd.airzip.filesecure.azs", ".azs", "IANA: AirZip", true);
                case "azw":
                case "application/vnd.amazon.ebook":
                    return ("Amazon Kindle eBook format", "application/vnd.amazon.ebook", ".azw",
                        "Kindle Direct Publishing", true);
                case "ami":
                case "application/vnd.amiga.ami":
                    return ("AmigaDE", "application/vnd.amiga.ami", ".ami", "IANA: Amiga", true);
                case "N/A":
                case "application/andrew-inset":
                    return ("Andrew Toolkit", "application/andrew-inset", "N/A", "IANA - Andrew Inset", true);
                case "apk":
                case "application/vnd.android.package-archive":
                    return ("Android Package Archive", "application/vnd.android.package-archive", ".apk",
                        "Wikipedia: APK File Format", true);
                case "cii":
                case "application/vnd.anser-web-certificate-issue-initiation":
                    return ("ANSER-WEB Terminal Client - Certificate Issue",
                        "application/vnd.anser-web-certificate-issue-initiation", ".cii", "IANA: ANSWER-WEB", true);
                case "fti":
                case "application/vnd.anser-web-funds-transfer-initiation":
                    return ("ANSER-WEB Terminal Client - Web Funds Transfer",
                        "application/vnd.anser-web-funds-transfer-initiation", ".fti", "IANA: ANSWER-WEB", true);
                case "atx":
                case "application/vnd.antix.game-component":
                    return ("Antix Game Player", "application/vnd.antix.game-component", ".atx",
                        "IANA: Antix Game Component", true);
                case "dmg":
                case "application/x-apple-diskimage":
                    return ("Apple Disk Image", "application/x-apple-diskimage", ".dmg", "Apple Disk Image", true);
                case "mpkg":
                case "application/vnd.apple.installer+xml":
                    return ("Apple Installer Package", "application/vnd.apple.installer+xml", ".mpkg",
                        "IANA: Apple Installer", true);
                case "aw":
                case "application/applixware":
                    return ("Applixware", "application/applixware", ".aw", "Vistasource", true);
                case "les":
                case "application/vnd.hhe.lesson-player":
                    return ("Archipelago Lesson Player", "application/vnd.hhe.lesson-player", ".les",
                        "IANA: Archipelago Lesson Player", true);
                case "swi":
                case "application/vnd.aristanetworks.swi":
                    return ("Arista Networks Software Image", "application/vnd.aristanetworks.swi", ".swi",
                        "IANA: Arista Networks Software Image", true);
                case "s":
                case "text/x-asm": return ("Assembler Source File", "text/x-asm", ".s", "Wikipedia: Assembly", true);
                case "atomcat":
                case "application/atomcat+xml":
                    return ("Atom Publishing Protocol", "application/atomcat+xml", ".atomcat", "RFC 5023", true);
                case "atomsvc":
                case "application/atomsvc+xml":
                    return ("Atom Publishing Protocol Service Document", "application/atomsvc+xml", ".atomsvc",
                        "RFC 5023", true);
                case "atom, .xml":
                case "application/atom+xml":
                    return ("Atom Syndication Format", "application/atom+xml", ".atom, .xml", "RFC 4287", true);
                case "ac":
                case "application/pkix-attr-cert":
                    return ("Attribute Certificate", "application/pkix-attr-cert", ".ac", "RFC 5877", true);
                case "aif":
                case "audio/x-aiff":
                    return ("Audio Interchange File Format", "audio/x-aiff", ".aif",
                        "Wikipedia: Audio Interchange File Format", true);
                case "avi":
                case "video/x-msvideo":
                    return ("Audio Video Interleave (AVI);", "video/x-msvideo", ".avi", "Wikipedia: AVI", true);
                case "aep":
                case "application/vnd.audiograph":
                    return ("Audiograph", "application/vnd.audiograph", ".aep", "IANA: Audiograph", true);
                case "dxf":
                case "image/vnd.dxf": return ("AutoCAD DXF", "image/vnd.dxf", ".dxf", "Wikipedia: AutoCAD DXF", true);
                case "dwf":
                case "model/vnd.dwf":
                    return ("Autodesk Design Web Format (DWF);", "model/vnd.dwf", ".dwf",
                        "Wikipedia: Design Web Format", true);
                case "par":
                case "text/plain-bas": return ("BAS Partitur Format", "text/plain-bas", ".par", "Phonetik BAS", true);
                case "bcpio":
                case "application/x-bcpio":
                    return ("Binary CPIO Archive", "application/x-bcpio", ".bcpio", "Wikipedia: cpio", true);
                case "bin":
                case CONTENT_BINARY: return ("Binary Data", CONTENT_BINARY, ".bin", null, true);

                case "bmp":
                case "image/bmp": return ("Bitmap Image File", "image/bmp", ".bmp", "Wikipedia: BMP File Format", true);
                case "torrent":
                case "application/x-bittorrent":
                    return ("BitTorrent", "application/x-bittorrent", ".torrent", "Wikipedia: BitTorrent", true);
                case "cod":
                case "application/vnd.rim.cod":
                    return ("Blackberry COD File", "application/vnd.rim.cod", ".cod", null, true);

                case "mpm":
                case "application/vnd.blueice.multipass":
                    return ("Blueice Research Multipass", "application/vnd.blueice.multipass", ".mpm",
                        "IANA: Multipass", true);
                case "bmi":
                case "application/vnd.bmi":
                    return ("BMI Drawing Data Interchange", "application/vnd.bmi", ".bmi", "IANA: BMI", true);
                case "sh":
                case "application/x-sh":
                    return ("Bourne Shell Script", "application/x-sh", ".sh", "Wikipedia: Bourne Shell", true);
                case "btif":
                case "image/prs.btif": return ("BTIF", "image/prs.btif", ".btif", "IANA: BTIF", true);
                case "rep":
                case "application/vnd.businessobjects":
                    return ("BusinessObjects", "application/vnd.businessobjects", ".rep", "IANA: BusinessObjects",
                        true);
                case "bz":
                case "application/x-bzip":
                    return ("Bzip Archive", "application/x-bzip", ".bz", "Wikipedia: Bzip", true);
                case "bz2":
                case "application/x-bzip2":
                    return ("Bzip2 Archive", "application/x-bzip2", ".bz2", "Wikipedia: Bzip", true);
                case "csh":
                case "application/x-csh":
                    return ("C Shell Script", "application/x-csh", ".csh", "Wikipedia: C Shell", true);
                case "c":
                case "text/x-c": return ("C Source File", "text/x-c", ".c", "Wikipedia: C Programming Language", true);
                case "cdxml":
                case "application/vnd.chemdraw+xml":
                    return ("CambridgeSoft Chem Draw", "application/vnd.chemdraw+xml", ".cdxml", "IANA: Chem Draw",
                        true);
                case "css":
                case "text/css": return ("Cascading Style Sheets (CSS);", "text/css", ".css", "Wikipedia: CSS", true);
                case "cdx":
                case "chemical/x-cdx":
                    return ("ChemDraw eXchange file", "chemical/x-cdx", ".cdx", "ChemDraw eXchange file", true);
                case "cml":
                case "chemical/x-cml":
                    return ("Chemical Markup Language", "chemical/x-cml", ".cml", "Wikipedia: Chemical Markup Language",
                        true);
                case "csml":
                case "chemical/x-csml":
                    return ("Chemical Style Markup Language", "chemical/x-csml", ".csml",
                        "Wikipedia: Chemical Style Markup Language", true);
                case "cdbcmsg":
                case "application/vnd.contact.cmsg":
                    return ("CIM Database", "application/vnd.contact.cmsg", ".cdbcmsg", "IANA: CIM Database", true);
                case "cla":
                case "application/vnd.claymore":
                    return ("Claymore Data Files", "application/vnd.claymore", ".cla", "IANA: Claymore", true);
                case "c4g":
                case "application/vnd.clonk.c4group":
                    return ("Clonk Game", "application/vnd.clonk.c4group", ".c4g", "IANA: Clonk", true);
                case "sub":
                case "image/vnd.dvb.subtitle":
                    return ("Close Captioning - Subtitle", "image/vnd.dvb.subtitle", ".sub",
                        "Wikipedia: Closed Captioning", true);
                case "cdmia":
                case "application/cdmi-capability":
                    return ("Cloud Data Management Interface (CDMI); - Capability", "application/cdmi-capability",
                        ".cdmia", "RFC 6208", true);
                case "cdmic":
                case "application/cdmi-container":
                    return ("Cloud Data Management Interface (CDMI); - Contaimer", "application/cdmi-container",
                        ".cdmic", "RFC 6209", true);
                case "cdmid":
                case "application/cdmi-domain":
                    return ("Cloud Data Management Interface (CDMI); - Domain", "application/cdmi-domain", ".cdmid",
                        "RFC 6210", true);
                case "cdmio":
                case "application/cdmi-object":
                    return ("Cloud Data Management Interface (CDMI); - Object", "application/cdmi-object", ".cdmio",
                        "RFC 6211", true);
                case "cdmiq":
                case "application/cdmi-queue":
                    return ("Cloud Data Management Interface (CDMI); - Queue", "application/cdmi-queue", ".cdmiq",
                        "RFC 6212", true);
                case "c11amc":
                case "application/vnd.cluetrust.cartomobile-config":
                    return ("ClueTrust CartoMobile - Config", "application/vnd.cluetrust.cartomobile-config", ".c11amc",
                        "IANA: CartoMobile", true);
                case "c11amz":
                case "application/vnd.cluetrust.cartomobile-config-pkg":
                    return ("ClueTrust CartoMobile - Config Package",
                        "application/vnd.cluetrust.cartomobile-config-pkg", ".c11amz", "IANA: CartoMobile", true);
                case "ras":
                case "image/x-cmu-raster": return ("CMU Image", "image/x-cmu-raster", ".ras", null, true);

                case "dae":
                case "model/vnd.collada+xml":
                    return ("COLLADA", "model/vnd.collada+xml", ".dae", "IANA: COLLADA", true);
                case "csv":
                case "text/csv": return ("Comma-Seperated Values", "text/csv", ".csv", "Wikipedia: CSV", true);
                case "cpt":
                case "application/mac-compactpro":
                    return ("Compact Pro", "application/mac-compactpro", ".cpt", "Compact Pro", true);
                case "wmlc":
                case "application/vnd.wap.wmlc":
                    return ("Compiled Wireless Markup Language (WMLC);", "application/vnd.wap.wmlc", ".wmlc",
                        "IANA: WMLC", true);
                case "cgm":
                case "image/cgm":
                    return ("Computer Graphics Metafile", "image/cgm", ".cgm", "Wikipedia: Computer Graphics Metafile",
                        true);
                case "ice":
                case "x-conference/x-cooltalk":
                    return ("CoolTalk", "x-conference/x-cooltalk", ".ice", "Wikipedia: CoolTalk", true);
                case "cmx":
                case "image/x-cmx":
                    return ("Corel Metafile Exchange (CMX);", "image/x-cmx", ".cmx", "Wikipedia: CorelDRAW", true);
                case "xar":
                case "application/vnd.xara":
                    return ("CorelXARA", "application/vnd.xara", ".xar", "IANA: CorelXARA", true);
                case "cmc":
                case "application/vnd.cosmocaller":
                    return ("CosmoCaller", "application/vnd.cosmocaller", ".cmc", "IANA: CosmoCaller", true);
                case "cpio":
                case "application/x-cpio":
                    return ("CPIO Archive", "application/x-cpio", ".cpio", "Wikipedia: cpio", true);
                case "clkx":
                case "application/vnd.crick.clicker":
                    return ("CrickSoftware - Clicker", "application/vnd.crick.clicker", ".clkx", "IANA: Clicker", true);
                case "clkk":
                case "application/vnd.crick.clicker.keyboard":
                    return ("CrickSoftware - Clicker - Keyboard", "application/vnd.crick.clicker.keyboard", ".clkk",
                        "IANA: Clicker", true);
                case "clkp":
                case "application/vnd.crick.clicker.palette":
                    return ("CrickSoftware - Clicker - Palette", "application/vnd.crick.clicker.palette", ".clkp",
                        "IANA: Clicker", true);
                case "clkt":
                case "application/vnd.crick.clicker.template":
                    return ("CrickSoftware - Clicker - Template", "application/vnd.crick.clicker.template", ".clkt",
                        "IANA: Clicker", true);
                case "clkw":
                case "application/vnd.crick.clicker.wordbank":
                    return ("CrickSoftware - Clicker - Wordbank", "application/vnd.crick.clicker.wordbank", ".clkw",
                        "IANA: Clicker", true);
                case "wbs":
                case "application/vnd.criticaltools.wbs+xml":
                    return ("Critical Tools - PERT Chart EXPERT", "application/vnd.criticaltools.wbs+xml", ".wbs",
                        "IANA: Critical Tools", true);
                case "cryptonote":
                case "application/vnd.rig.cryptonote":
                    return ("CryptoNote", "application/vnd.rig.cryptonote", ".cryptonote", "IANA: CryptoNote", true);
                case "cif":
                case "chemical/x-cif":
                    return ("Crystallographic Interchange Format", "chemical/x-cif", ".cif",
                        "Crystallographic Interchange Format", true);
                case "cmdf":
                case "chemical/x-cmdf":
                    return ("CrystalMaker Data Format", "chemical/x-cmdf", ".cmdf", "CrystalMaker Data Format", true);
                case "cu":
                case "application/cu-seeme": return ("CU-SeeMe", "application/cu-seeme", ".cu", "White Pine", true);
                case "cww":
                case "application/prs.cww": return ("CU-Writer", "application/prs.cww", ".cww", null, true);

                case ".curl":
                case "text/vnd.curl": return ("Curl - Applet", "text/vnd.curl", ".curl", "Curl Applet", true);
                case "dcurl":
                case "text/vnd.curl.dcurl":
                    return ("Curl - Detached Applet", "text/vnd.curl.dcurl", ".dcurl", "Curl Detached Applet", true);
                case "mcurl":
                case "text/vnd.curl.mcurl":
                    return ("Curl - Manifest File", "text/vnd.curl.mcurl", ".mcurl", "Curl Manifest File", true);
                case "scurl":
                case "text/vnd.curl.scurl":
                    return ("Curl - Source Code", "text/vnd.curl.scurl", ".scurl", "Curl Source Code", true);
                case "car":
                case "application/vnd.curl.car":
                    return ("CURL Applet", "application/vnd.curl.car", ".car", "IANA: CURL Applet", true);
                case "pcurl":
                case "application/vnd.curl.pcurl":
                    return ("CURL Applet", "application/vnd.curl.pcurl", ".pcurl", "IANA: CURL Applet", true);
                case "cmp":
                case "application/vnd.yellowriver-custom-menu":
                    return ("CustomMenu", "application/vnd.yellowriver-custom-menu", ".cmp", "IANA: CustomMenu", true);
                case "dssc":
                case "application/dssc+der":
                    return ("Data Structure for the Security Suitability of Cryptographic Algorithms",
                        "application/dssc+der", ".dssc", "RFC 5698", true);
                case "xdssc":
                case "application/dssc+xml":
                    return ("Data Structure for the Security Suitability of Cryptographic Algorithms",
                        "application/dssc+xml", ".xdssc", "RFC 5698", true);
                case "deb":
                case "application/x-debian-package":
                    return ("Debian Package", "application/x-debian-package", ".deb", "Wikipedia: Debian Package",
                        true);
                case "uva":
                case "audio/vnd.dece.audio":
                    return ("DECE Audio", "audio/vnd.dece.audio", ".uva", "IANA: Dece Audio", true);
                case "uvi":
                case "image/vnd.dece.graphic":
                    return ("DECE Graphic", "image/vnd.dece.graphic", ".uvi", "IANA: DECE Graphic", true);
                case "uvh":
                case "video/vnd.dece.hd":
                    return ("DECE High Definition Video", "video/vnd.dece.hd", ".uvh", "IANA: DECE HD Video", true);
                case "uvm":
                case "video/vnd.dece.mobile":
                    return ("DECE Mobile Video", "video/vnd.dece.mobile", ".uvm", "IANA: DECE Mobile Video", true);
                case "uvu":
                case "video/vnd.uvvu.mp4": return ("DECE MP4", "video/vnd.uvvu.mp4", ".uvu", "IANA: DECE MP4", true);
                case "uvp":
                case "video/vnd.dece.pd":
                    return ("DECE PD Video", "video/vnd.dece.pd", ".uvp", "IANA: DECE PD Video", true);
                case "uvs":
                case "video/vnd.dece.sd":
                    return ("DECE SD Video", "video/vnd.dece.sd", ".uvs", "IANA: DECE SD Video", true);
                case "uvv":
                case "video/vnd.dece.video":
                    return ("DECE Video", "video/vnd.dece.video", ".uvv", "IANA: DECE Video", true);
                case "dvi":
                case "application/x-dvi":
                    return ("Device Independent File Format (DVI);", "application/x-dvi", ".dvi", "Wikipedia: DVI",
                        true);
                case "seed":
                case "application/vnd.fdsn.seed":
                    return ("Digital Siesmograph Networks - SEED Datafiles", "application/vnd.fdsn.seed", ".seed",
                        "IANA: SEED", true);
                case "dtb":
                case "application/x-dtbook+xml":
                    return ("Digital Talking Book", "application/x-dtbook+xml", ".dtb", "Wikipedia: EPUB", true);
                case "res":
                case "application/x-dtbresource+xml":
                    return ("Digital Talking Book - Resource File", "application/x-dtbresource+xml", ".res",
                        "Digital Talking Book", true);
                case "ait":
                case "application/vnd.dvb.ait":
                    return ("Digital Video Broadcasting", "application/vnd.dvb.ait", ".ait",
                        "IANA: Digital Video Broadcasting", true);
                case "svc":
                case "application/vnd.dvb.service":
                    return ("Digital Video Broadcasting", "application/vnd.dvb.service", ".svc",
                        "IANA: Digital Video Broadcasting", true);
                case "eol":
                case "audio/vnd.digital-winds":
                    return ("Digital Winds Music", "audio/vnd.digital-winds", ".eol", "IANA: Digital Winds", true);
                case "djvu":
                case "image/vnd.djvu": return ("DjVu", "image/vnd.djvu", ".djvu", "Wikipedia: DjVu", true);
                case "dtd":
                case "application/xml-dtd":
                    return ("Document Type Definition", "application/xml-dtd", ".dtd", "W3C DTD", true);
                case "mlp":
                case "application/vnd.dolby.mlp":
                    return ("Dolby Meridian Lossless Packing", "application/vnd.dolby.mlp", ".mlp",
                        "IANA: Dolby Meridian Lossless Packing", true);
                case "wad":
                case "application/x-doom":
                    return ("Doom Video Game", "application/x-doom", ".wad", "Wikipedia: Doom WAD", true);
                case "dpg":
                case "application/vnd.dpgraph":
                    return ("DPGraph", "application/vnd.dpgraph", ".dpg", "IANA: DPGraph", true);
                case "dra":
                case "audio/vnd.dra": return ("DRA Audio", "audio/vnd.dra", ".dra", "IANA: DRA", true);
                case "dfac":
                case "application/vnd.dreamfactory":
                    return ("DreamFactory", "application/vnd.dreamfactory", ".dfac", "IANA: DreamFactory", true);
                case "dts":
                case "audio/vnd.dts": return ("DTS Audio", "audio/vnd.dts", ".dts", "IANA: DTS", true);
                case "dtshd":
                case "audio/vnd.dts.hd":
                    return ("DTS High Definition Audio", "audio/vnd.dts.hd", ".dtshd", "IANA: DTS HD", true);
                case "dwg":
                case "image/vnd.dwg": return ("DWG Drawing", "image/vnd.dwg", ".dwg", "Wikipedia: DWG", true);
                case "geo":
                case "application/vnd.dynageo":
                    return ("DynaGeo", "application/vnd.dynageo", ".geo", "IANA: DynaGeo", true);
                case "es":
                case "application/ecmascript": return ("ECMAScript", "application/ecmascript", ".es", "ECMA-357", true);
                case "mag":
                case "application/vnd.ecowin.chart":
                    return ("EcoWin Chart", "application/vnd.ecowin.chart", ".mag", "IANA: EcoWin Chart", true);
                case "mmr":
                case "image/vnd.fujixerox.edmics-mmr":
                    return ("EDMICS 2000", "image/vnd.fujixerox.edmics-mmr", ".mmr", "IANA: EDMICS 2000", true);
                case "rlc":
                case "image/vnd.fujixerox.edmics-rlc":
                    return ("EDMICS 2000", "image/vnd.fujixerox.edmics-rlc", ".rlc", "IANA: EDMICS 2000", true);
                case "exi":
                case "application/exi":
                    return ("Efficient XML Interchange", "application/exi", ".exi",
                        "Efficient XML Interchange (EXI); Best Practices", true);
                case "mgz":
                case "application/vnd.proteus.magazine":
                    return ("EFI Proteus", "application/vnd.proteus.magazine", ".mgz", "IANA: EFI Proteus", true);
                case "epub":
                case "application/epub+zip":
                    return ("Electronic Publication", "application/epub+zip", ".epub", "Wikipedia: EPUB", true);
                case "eml":
                case "message/rfc822": return ("Email Message", "message/rfc822", ".eml", "RFC 2822", true);
                case "nml":
                case "application/vnd.enliven":
                    return ("Enliven Viewer", "application/vnd.enliven", ".nml", "IANA: Enliven Viewer", true);
                case "xpr":
                case "application/vnd.is-xpr":
                    return ("Express by Infoseek", "application/vnd.is-xpr", ".xpr", "IANA: Express by Infoseek", true);
                case "xif":
                case "image/vnd.xiff":
                    return ("eXtended Image File Format (XIFF);", "image/vnd.xiff", ".xif", "IANA: XIFF", true);
                case "xfdl":
                case "application/vnd.xfdl":
                    return ("Extensible Forms Description Language", "application/vnd.xfdl", ".xfdl",
                        "IANA: Extensible Forms Description Language", true);
                case "emma":
                case "application/emma+xml":
                    return ("Extensible MultiModal Annotation", "application/emma+xml", ".emma",
                        "EMMA: Extensible MultiModal Annotation markup language", true);
                case "ez2":
                case "application/vnd.ezpix-album":
                    return ("EZPix Secure Photo Album", "application/vnd.ezpix-album", ".ez2",
                        "IANA: EZPix Secure Photo Album", true);
                case "ez3":
                case "application/vnd.ezpix-package":
                    return ("EZPix Secure Photo Album", "application/vnd.ezpix-package", ".ez3",
                        "IANA: EZPix Secure Photo Album", true);
                case "fst":
                case "image/vnd.fst":
                    return ("FAST Search & Transfer ASA", "image/vnd.fst", ".fst", "IANA: FAST Search & Transfer ASA",
                        true);
                case "fvt":
                case "video/vnd.fvt": return ("FAST Search & Transfer ASA", "video/vnd.fvt", ".fvt", "IANA: FVT", true);
                case "fbs":
                case "image/vnd.fastbidsheet":
                    return ("FastBid Sheet", "image/vnd.fastbidsheet", ".fbs", "IANA: FastBid Sheet", true);
                case "fe_launch":
                case "application/vnd.denovo.fcselayout-link":
                    return ("FCS Express Layout Link", "application/vnd.denovo.fcselayout-link", ".fe_launch",
                        "IANA: FCS Express Layout Link", true);
                case "f4v":
                case "video/x-f4v": return ("Flash Video", "video/x-f4v", ".f4v", "Wikipedia: Flash Video", true);
                case "flv":
                case "video/x-flv": return ("Flash Video", "video/x-flv", ".flv", "Wikipedia: Flash Video", true);
                case "fpx":
                case "image/vnd.fpx": return ("FlashPix", "image/vnd.fpx", ".fpx", "IANA: FPX", true);
                case "npx":
                case "image/vnd.net-fpx": return ("FlashPix", "image/vnd.net-fpx", ".npx", "IANA: FPX", true);
                case "flx":
                case "text/vnd.fmi.flexstor":
                    return ("FLEXSTOR", "text/vnd.fmi.flexstor", ".flx", "IANA: FLEXSTOR", true);
                case "fli":
                case "video/x-fli":
                    return ("FLI/FLC Animation Format", "video/x-fli", ".fli", "FLI/FLC Animation Format", true);
                case "ftc":
                case "application/vnd.fluxtime.clip":
                    return ("FluxTime Clip", "application/vnd.fluxtime.clip", ".ftc", "IANA: FluxTime Clip", true);
                case "fdf":
                case "application/vnd.fdf":
                    return ("Forms Data Format", "application/vnd.fdf", ".fdf", "IANA: Forms Data Format", true);
                case "f":
                case "text/x-fortran":
                    return ("Fortran Source File", "text/x-fortran", ".f", "Wikipedia: Fortran", true);
                case "mif":
                case "application/vnd.mif":
                    return ("FrameMaker Interchange Format", "application/vnd.mif", ".mif",
                        "IANA: FrameMaker Interchange Format", true);
                case "fm":
                case "application/vnd.framemaker":
                    return ("FrameMaker Normal Format", "application/vnd.framemaker", ".fm", "IANA: FrameMaker", true);
                case "fh":
                case "image/x-freehand":
                    return ("FreeHand MX", "image/x-freehand", ".fh", "Wikipedia: Macromedia Freehand", true);
                case "fsc":
                case "application/vnd.fsc.weblaunch":
                    return ("Friendly Software Corporation", "application/vnd.fsc.weblaunch", ".fsc",
                        "IANA: Friendly Software Corporation", true);
                case "fnc":
                case "application/vnd.frogans.fnc":
                    return ("Frogans Player", "application/vnd.frogans.fnc", ".fnc", "IANA: Frogans Player", true);
                case "ltf":
                case "application/vnd.frogans.ltf":
                    return ("Frogans Player", "application/vnd.frogans.ltf", ".ltf", "IANA: Frogans Player", true);
                case "ddd":
                case "application/vnd.fujixerox.ddd":
                    return ("Fujitsu - Xerox 2D CAD Data", "application/vnd.fujixerox.ddd", ".ddd", "IANA: Fujitsu DDD",
                        true);
                case "xdw":
                case "application/vnd.fujixerox.docuworks":
                    return ("Fujitsu - Xerox DocuWorks", "application/vnd.fujixerox.docuworks", ".xdw",
                        "IANA: Docuworks", true);
                case "xbd":
                case "application/vnd.fujixerox.docuworks.binder":
                    return ("Fujitsu - Xerox DocuWorks Binder", "application/vnd.fujixerox.docuworks.binder", ".xbd",
                        "IANA: Docuworks Binder", true);
                case "oas":
                case "application/vnd.fujitsu.oasys":
                    return ("Fujitsu Oasys", "application/vnd.fujitsu.oasys", ".oas", "IANA: Fujitsu Oasys", true);
                case "oa2":
                case "application/vnd.fujitsu.oasys2":
                    return ("Fujitsu Oasys", "application/vnd.fujitsu.oasys2", ".oa2", "IANA: Fujitsu Oasys", true);
                case "oa3":
                case "application/vnd.fujitsu.oasys3":
                    return ("Fujitsu Oasys", "application/vnd.fujitsu.oasys3", ".oa3", "IANA: Fujitsu Oasys", true);
                case "fg5":
                case "application/vnd.fujitsu.oasysgp":
                    return ("Fujitsu Oasys", "application/vnd.fujitsu.oasysgp", ".fg5", "IANA: Fujitsu Oasys", true);
                case "bh2":
                case "application/vnd.fujitsu.oasysprs":
                    return ("Fujitsu Oasys", "application/vnd.fujitsu.oasysprs", ".bh2", "IANA: Fujitsu Oasys", true);
                case "spl":
                case "application/x-futuresplash":
                    return ("FutureSplash Animator", "application/x-futuresplash", ".spl",
                        "Wikipedia: FutureSplash Animator", true);
                case "fzs":
                case "application/vnd.fuzzysheet":
                    return ("FuzzySheet", "application/vnd.fuzzysheet", ".fzs", "IANA: FuzySheet", true);
                case "g3":
                case "image/g3fax": return ("G3 Fax Image", "image/g3fax", ".g3", "Wikipedia: G3 Fax Image", true);
                case "gmx":
                case "application/vnd.gmx":
                    return ("GameMaker ActiveX", "application/vnd.gmx", ".gmx", "IANA: GameMaker ActiveX", true);
                case "gtw":
                case "model/vnd.gtw": return ("Gen-Trix Studio", "model/vnd.gtw", ".gtw", "IANA: GTW", true);
                case "txd":
                case "application/vnd.genomatix.tuxedo":
                    return ("Genomatix Tuxedo Framework", "application/vnd.genomatix.tuxedo", ".txd",
                        "IANA: Genomatix Tuxedo Framework", true);
                case "ggb":
                case "application/vnd.geogebra.file":
                    return ("GeoGebra", "application/vnd.geogebra.file", ".ggb", "IANA: GeoGebra", true);
                case "ggt":
                case "application/vnd.geogebra.tool":
                    return ("GeoGebra", "application/vnd.geogebra.tool", ".ggt", "IANA: GeoGebra", true);
                case "gdl":
                case "model/vnd.gdl":
                    return ("Geometric Description Language (GDL);", "model/vnd.gdl", ".gdl", "IANA: GDL", true);
                case "gex":
                case "application/vnd.geometry-explorer":
                    return ("GeoMetry Explorer", "application/vnd.geometry-explorer", ".gex", "IANA: GeoMetry Explorer",
                        true);
                case "gxt":
                case "application/vnd.geonext":
                    return ("GEONExT and JSXGraph", "application/vnd.geonext", ".gxt", "IANA: GEONExT and JSXGraph",
                        true);
                case "g2w":
                case "application/vnd.geoplan":
                    return ("GeoplanW", "application/vnd.geoplan", ".g2w", "IANA: GeoplanW", true);
                case "g3w":
                case "application/vnd.geospace":
                    return ("GeospacW", "application/vnd.geospace", ".g3w", "IANA: GeospacW", true);
                case "gsf":
                case "application/x-font-ghostscript":
                    return ("Ghostscript Font", "application/x-font-ghostscript", ".gsf", "Wikipedia: Ghostscript",
                        true);
                case "bdf":
                case "application/x-font-bdf":
                    return ("Glyph Bitmap Distribution Format", "application/x-font-bdf", ".bdf",
                        "Wikipedia: Glyph Bitmap Distribution Format", true);
                case "gtar":
                case "application/x-gtar": return ("GNU Tar Files", "application/x-gtar", ".gtar", "GNU Tar", true);
                case "texinfo":
                case "application/x-texinfo":
                    return ("GNU Texinfo Document", "application/x-texinfo", ".texinfo", "Wikipedia: Texinfo", true);
                case "gnumeric":
                case "application/x-gnumeric":
                    return ("Gnumeric", "application/x-gnumeric", ".gnumeric", "Wikipedia: Gnumeric", true);
                case "kml":
                case "application/vnd.google-earth.kml+xml":
                    return ("Google Earth - KML", "application/vnd.google-earth.kml+xml", ".kml", "IANA: Google Earth",
                        true);
                case "kmz":
                case "application/vnd.google-earth.kmz":
                    return ("Google Earth - Zipped KML", "application/vnd.google-earth.kmz", ".kmz",
                        "IANA: Google Earth", true);
                case "gqf":
                case "application/vnd.grafeq":
                    return ("GrafEq", "application/vnd.grafeq", ".gqf", "IANA: GrafEq", true);
                case "gif":
                case "image/gif":
                    return ("Graphics Interchange Format", "image/gif", ".gif",
                        "Wikipedia: Graphics Interchange Format", false);
                case "gv":
                case "text/vnd.graphviz": return ("Graphviz", "text/vnd.graphviz", ".gv", "IANA: Graphviz", true);
                case "gac":
                case "application/vnd.groove-account":
                    return ("Groove - Account", "application/vnd.groove-account", ".gac", "IANA: Groove", true);
                case "ghf":
                case "application/vnd.groove-help":
                    return ("Groove - Help", "application/vnd.groove-help", ".ghf", "IANA: Groove", true);
                case "gim":
                case "application/vnd.groove-identity-message":
                    return ("Groove - Identity Message", "application/vnd.groove-identity-message", ".gim",
                        "IANA: Groove", true);
                case "grv":
                case "application/vnd.groove-injector":
                    return ("Groove - Injector", "application/vnd.groove-injector", ".grv", "IANA: Groove", true);
                case "gtm":
                case "application/vnd.groove-tool-message":
                    return ("Groove - Tool Message", "application/vnd.groove-tool-message", ".gtm", "IANA: Groove",
                        true);
                case "tpl":
                case "application/vnd.groove-tool-template":
                    return ("Groove - Tool Template", "application/vnd.groove-tool-template", ".tpl", "IANA: Groove",
                        true);
                case "vcg":
                case "application/vnd.groove-vcard":
                    return ("Groove - Vcard", "application/vnd.groove-vcard", ".vcg", "IANA: Groove", true);
                case "h261":
                case "video/h261": return ("H.261", "video/h261", ".h261", "Wikipedia: H.261", true);
                case "h263":
                case "video/h263": return ("H.263", "video/h263", ".h263", "Wikipedia: H.263", true);
                case "h264":
                case "video/h264": return ("H.264", "video/h264", ".h264", "Wikipedia: H.264", true);
                case "hpid":
                case "application/vnd.hp-hpid":
                    return ("Hewlett Packard Instant Delivery", "application/vnd.hp-hpid", ".hpid",
                        "IANA: Hewlett Packard Instant Delivery", true);
                case "hps":
                case "application/vnd.hp-hps":
                    return ("Hewlett-Packard's WebPrintSmart", "application/vnd.hp-hps", ".hps",
                        "IANA: Hewlett-Packard's WebPrintSmart", true);
                case "hdf":
                case "application/x-hdf":
                    return ("Hierarchical Data Format", "application/x-hdf", ".hdf",
                        "Wikipedia: Hierarchical Data Format", true);
                case "rip":
                case "audio/vnd.rip": return ("Hit'n'Mix", "audio/vnd.rip", ".rip", "IANA: Hit'n'Mix", true);
                case "hbci":
                case "application/vnd.hbci":
                    return ("Homebanking Computer Interface (HBCI);", "application/vnd.hbci", ".hbci", "IANA: HBCI",
                        true);
                case "jlt":
                case "application/vnd.hp-jlyt":
                    return ("HP Indigo Digital Press - Job Layout Languate", "application/vnd.hp-jlyt", ".jlt",
                        "IANA: HP Job Layout Language", true);
                case "pcl":
                case "application/vnd.hp-pcl":
                    return ("HP Printer Command Language", "application/vnd.hp-pcl", ".pcl",
                        "IANA: HP Printer Command Language", true);
                case "hpgl":
                case "application/vnd.hp-hpgl":
                    return ("HP-GL/2 and HP RTL", "application/vnd.hp-hpgl", ".hpgl", "IANA: HP-GL/2 and HP RTL", true);
                case "hvs":
                case "application/vnd.yamaha.hv-script":
                    return ("HV Script", "application/vnd.yamaha.hv-script", ".hvs", "IANA: HV Script", true);
                case "hvd":
                case "application/vnd.yamaha.hv-dic":
                    return ("HV Voice Dictionary", "application/vnd.yamaha.hv-dic", ".hvd", "IANA: HV Voice Dictionary",
                        true);
                case "hvp":
                case "application/vnd.yamaha.hv-voice":
                    return ("HV Voice Parameter", "application/vnd.yamaha.hv-voice", ".hvp", "IANA: HV Voice Parameter",
                        true);
                case "sfd-hdstx":
                case "application/vnd.hydrostatix.sof-data":
                    return ("Hydrostatix Master Suite", "application/vnd.hydrostatix.sof-data", ".sfd-hdstx",
                        "IANA: Hydrostatix Master Suite", true);
                case "stk":
                case "application/hyperstudio":
                    return ("Hyperstudio", "application/hyperstudio", ".stk", "IANA - Hyperstudio", true);
                case "hal":
                case "application/vnd.hal+xml":
                    return ("Hypertext Application Language", "application/vnd.hal+xml", ".hal", "IANA: HAL", true);
                case "html":
                case "text/html":
                    return ("HyperText Markup Language (HTML);", "text/html", ".html", "Wikipedia: HTML", false);
                case "irm":
                case "application/vnd.ibm.rights-management":
                    return ("IBM DB2 Rights Manager", "application/vnd.ibm.rights-management", ".irm",
                        "IANA: IBM DB2 Rights Manager", true);
                case "sc":
                case "application/vnd.ibm.secure-container":
                    return ("IBM Electronic Media Management System - Secure Container",
                        "application/vnd.ibm.secure-container", ".sc", "IANA: EMMS", true);
                case "ics":
                case "text/calendar": return ("iCalendar", "text/calendar", ".ics", "Wikipedia: iCalendar", true);
                case "icc":
                case "application/vnd.iccprofile":
                    return ("ICC profile", "application/vnd.iccprofile", ".icc", "IANA: ICC profile", true);
                case "ico":
                case "image/x-icon": return ("Icon Image", "image/x-icon", ".ico", "Wikipedia: ICO File Format", true);
                case "igl":
                case "application/vnd.igloader":
                    return ("igLoader", "application/vnd.igloader", ".igl", "IANA: igLoader", true);
                case "ief":
                case "image/ief": return ("Image Exchange Format", "image/ief", ".ief", "RFC 1314", true);
                case "ivp":
                case "application/vnd.immervision-ivp":
                    return ("ImmerVision PURE Players", "application/vnd.immervision-ivp", ".ivp",
                        "IANA: ImmerVision PURE Players", true);
                case "ivu":
                case "application/vnd.immervision-ivu":
                    return ("ImmerVision PURE Players", "application/vnd.immervision-ivu", ".ivu",
                        "IANA: ImmerVision PURE Players", true);
                case "rif":
                case "application/reginfo+xml": return ("IMS Networks", "application/reginfo+xml", ".rif", null, true);
                case "3dml":
                case "text/vnd.in3d.3dml": return ("In3D - 3DML", "text/vnd.in3d.3dml", ".3dml", "IANA: In3D", true);
                case "spot":
                case "text/vnd.in3d.spot": return ("In3D - 3DML", "text/vnd.in3d.spot", ".spot", "IANA: In3D", true);
                case "igs":
                case "model/iges":
                    return ("Initial Graphics Exchange Specification (IGES);", "model/iges", ".igs", "Wikipedia: IGES",
                        true);
                case "i2g":
                case "application/vnd.intergeo":
                    return ("Interactive Geometry Software", "application/vnd.intergeo", ".i2g",
                        "IANA: Interactive Geometry Software", true);
                case "cdy":
                case "application/vnd.cinderella":
                    return ("Interactive Geometry Software Cinderella", "application/vnd.cinderella", ".cdy",
                        "IANA: Cinderella", true);
                case "xpw":
                case "application/vnd.intercon.formnet":
                    return ("Intercon FormNet", "application/vnd.intercon.formnet", ".xpw", "IANA: Intercon FormNet",
                        true);
                case "fcs":
                case "application/vnd.isac.fcs":
                    return ("International Society for Advancement of Cytometry", "application/vnd.isac.fcs", ".fcs",
                        "IANA: International Society for Advancement of Cytometry", true);
                case "ipfix":
                case "application/ipfix":
                    return ("Internet Protocol Flow Information Export", "application/ipfix", ".ipfix", "RFC 3917",
                        true);
                case "cer":
                case "application/pkix-cert":
                    return ("Internet Public Key Infrastructure - Certificate", "application/pkix-cert", ".cer",
                        "RFC 2585", true);
                case "pki":
                case "application/pkixcmp":
                    return ("Internet Public Key Infrastructure - Certificate Management Protocole",
                        "application/pkixcmp", ".pki", "RFC 2585", true);
                case "crl":
                case "application/pkix-crl":
                    return ("Internet Public Key Infrastructure - Certificate Revocation Lists", "application/pkix-crl",
                        ".crl", "RFC 2585", true);
                case "pkipath":
                case "application/pkix-pkipath":
                    return ("Internet Public Key Infrastructure - Certification Path", "application/pkix-pkipath",
                        ".pkipath", "RFC 2585", true);
                case "igm":
                case "application/vnd.insors.igm":
                    return ("IOCOM Visimeet", "application/vnd.insors.igm", ".igm", "IANA: IOCOM Visimeet", true);
                case "rcprofile":
                case "application/vnd.ipunplugged.rcprofile":
                    return ("IP Unplugged Roaming Client", "application/vnd.ipunplugged.rcprofile", ".rcprofile",
                        "IANA: IP Unplugged Roaming Client", true);
                case "irp":
                case "application/vnd.irepository.package+xml":
                    return ("iRepository / Lucidoc Editor", "application/vnd.irepository.package+xml", ".irp",
                        "IANA: iRepository / Lucidoc Editor", true);
                case "jad":
                case "text/vnd.sun.j2me.app-descriptor":
                    return ("J2ME App Descriptor", "text/vnd.sun.j2me.app-descriptor", ".jad",
                        "IANA: J2ME App Descriptor", true);
                case "jar":
                case "application/java-archive":
                    return ("Java Archive", "application/java-archive", ".jar", "Wikipedia: JAR file format", true);
                case "class":
                case "application/java-vm":
                    return ("Java Bytecode File", "application/java-vm", ".class", "Wikipedia: Java Bytecode", true);
                case "jnlp":
                case "application/x-java-jnlp-file":
                    return ("Java Network Launching Protocol", "application/x-java-jnlp-file", ".jnlp",
                        "Wikipedia: Java Web Start", true);
                case "ser":
                case "application/java-serialized-object":
                    return ("Java Serialized Object", "application/java-serialized-object", ".ser",
                        "Java Serialization API", true);
                case "java":
                case "text/x-java-source,java":
                    return ("Java Source File", "text/x-java-source,java", ".java", "Wikipedia: Java", true);
                case "js":
                case "application/javascript":
                    return ("JavaScript", "application/javascript", ".js", "JavaScript", true);
                case "json":
                case "application/json":
                    return ("JavaScript Object Notation (JSON);", "application/json", ".json", "Wikipedia: JSON", true);
                case "joda":
                case "application/vnd.joost.joda-archive":
                    return ("Joda Archive", "application/vnd.joost.joda-archive", ".joda", "IANA: Joda Archive", true);
                case "jpm":
                case "video/jpm":
                    return ("JPEG 2000 Compound Image File Format", "video/jpm", ".jpm", "IANA: JPM", true);
                case "jpeg":
                case "jpg":
                case "image/jpeg": return ("JPEG Image", "image/jpeg", ".jpeg", "RFC 1314", false);
                case "image/x-citrix-jpeg":
                    return ("JPEG Image (Citrix client);", "image/x-citrix-jpeg", ".jpeg", "RFC 1314", false);
                case "pjpeg":
                case "image/pjpeg":
                    return ("JPEG Image (Progressive);", "image/pjpeg", ".pjpeg", "JPEG image compression FAQ", true);
                case "jpgv":
                case "video/jpeg": return ("JPGVideo", "video/jpeg", ".jpgv", "RFC 3555", true);
                case "ktz":
                case "application/vnd.kahootz":
                    return ("Kahootz", "application/vnd.kahootz", ".ktz", "IANA: Kahootz", true);
                case "mmd":
                case "application/vnd.chipnuts.karaoke-mmd":
                    return ("Karaoke on Chipnuts Chipsets", "application/vnd.chipnuts.karaoke-mmd", ".mmd",
                        "IANA: Chipnuts Karaoke", true);
                case "karbon":
                case "application/vnd.kde.karbon":
                    return ("KDE KOffice Office Suite - Karbon", "application/vnd.kde.karbon", ".karbon",
                        "IANA: KDE KOffice Office Suite", true);
                case "chrt":
                case "application/vnd.kde.kchart":
                    return ("KDE KOffice Office Suite - KChart", "application/vnd.kde.kchart", ".chrt",
                        "IANA: KDE KOffice Office Suite", true);
                case "kfo":
                case "application/vnd.kde.kformula":
                    return ("KDE KOffice Office Suite - Kformula", "application/vnd.kde.kformula", ".kfo",
                        "IANA: KDE KOffice Office Suite", true);
                case "flw":
                case "application/vnd.kde.kivio":
                    return ("KDE KOffice Office Suite - Kivio", "application/vnd.kde.kivio", ".flw",
                        "IANA: KDE KOffice Office Suite", true);
                case "kon":
                case "application/vnd.kde.kontour":
                    return ("KDE KOffice Office Suite - Kontour", "application/vnd.kde.kontour", ".kon",
                        "IANA: KDE KOffice Office Suite", true);
                case "kpr":
                case "application/vnd.kde.kpresenter":
                    return ("KDE KOffice Office Suite - Kpresenter", "application/vnd.kde.kpresenter", ".kpr",
                        "IANA: KDE KOffice Office Suite", true);
                case "ksp":
                case "application/vnd.kde.kspread":
                    return ("KDE KOffice Office Suite - Kspread", "application/vnd.kde.kspread", ".ksp",
                        "IANA: KDE KOffice Office Suite", true);
                case "kwd":
                case "application/vnd.kde.kword":
                    return ("KDE KOffice Office Suite - Kword", "application/vnd.kde.kword", ".kwd",
                        "IANA: KDE KOffice Office Suite", true);
                case "htke":
                case "application/vnd.kenameaapp":
                    return ("Kenamea App", "application/vnd.kenameaapp", ".htke", "IANA: Kenamea App", true);
                case "kia":
                case "application/vnd.kidspiration":
                    return ("Kidspiration", "application/vnd.kidspiration", ".kia", "IANA: Kidspiration", true);
                case "kne":
                case "application/vnd.kinar":
                    return ("Kinar Applications", "application/vnd.kinar", ".kne", "IANA: Kina Applications", true);
                case "sse":
                case "application/vnd.kodak-descriptor":
                    return ("Kodak Storyshare", "application/vnd.kodak-descriptor", ".sse", "IANA: Kodak Storyshare",
                        true);
                case "lasxml":
                case "application/vnd.las.las+xml":
                    return ("Laser App Enterprise", "application/vnd.las.las+xml", ".lasxml",
                        "IANA: Laser App Enterprise", true);
                case "latex":
                case "application/x-latex": return ("LaTeX", "application/x-latex", ".latex", "Wikipedia: LaTeX", true);
                case "lbd":
                case "application/vnd.llamagraphics.life-balance.desktop":
                    return ("Life Balance - Desktop Edition", "application/vnd.llamagraphics.life-balance.desktop",
                        ".lbd", "IANA: Life Balance", true);
                case "lbe":
                case "application/vnd.llamagraphics.life-balance.exchange+xml":
                    return ("Life Balance - Exchange Format", "application/vnd.llamagraphics.life-balance.exchange+xml",
                        ".lbe", "IANA: Life Balance", true);
                case "jam":
                case "application/vnd.jam":
                    return ("Lightspeed Audio Lab", "application/vnd.jam", ".jam", "IANA: Lightspeed Audio Lab", true);
                case "0.123":
                case "application/vnd.lotus-1-2-3":
                    return ("Lotus 1-2-3", "application/vnd.lotus-1-2-3", "0.123", "IANA: Lotus 1-2-3", true);
                case "apr":
                case "application/vnd.lotus-approach":
                    return ("Lotus Approach", "application/vnd.lotus-approach", ".apr", "IANA: Lotus Approach", true);
                case "pre":
                case "application/vnd.lotus-freelance":
                    return ("Lotus Freelance", "application/vnd.lotus-freelance", ".pre", "IANA: Lotus Freelance",
                        true);
                case "nsf":
                case "application/vnd.lotus-notes":
                    return ("Lotus Notes", "application/vnd.lotus-notes", ".nsf", "IANA: Lotus Notes", true);
                case "org":
                case "application/vnd.lotus-organizer":
                    return ("Lotus Organizer", "application/vnd.lotus-organizer", ".org", "IANA: Lotus Organizer",
                        true);
                case "scm":
                case "application/vnd.lotus-screencam":
                    return ("Lotus Screencam", "application/vnd.lotus-screencam", ".scm", "IANA: Lotus Screencam",
                        true);
                case "lwp":
                case "application/vnd.lotus-wordpro":
                    return ("Lotus Wordpro", "application/vnd.lotus-wordpro", ".lwp", "IANA: Lotus Wordpro", true);
                case "lvp":
                case "audio/vnd.lucent.voice":
                    return ("Lucent Voice", "audio/vnd.lucent.voice", ".lvp", "IANA: Lucent Voice", true);
                case "m3u":
                case "audio/x-mpegurl":
                    return ("M3U (Multimedia Playlist);", "audio/x-mpegurl", ".m3u", "Wikipedia: M3U", true);
                case "m4v":
                case "video/x-m4v": return ("M4v", "video/x-m4v", ".m4v", "Wikipedia: M4v", true);
                case "hqx":
                case "application/mac-binhex40":
                    return ("Macintosh BinHex 4.0", "application/mac-binhex40", ".hqx", "MacMIME", true);
                case "portpkg":
                case "application/vnd.macports.portpkg":
                    return ("MacPorts Port System", "application/vnd.macports.portpkg", ".portpkg",
                        "IANA: MacPorts Port System", true);
                case "mgp":
                case "application/vnd.osgeo.mapguide.package":
                    return ("MapGuide DBXML", "application/vnd.osgeo.mapguide.package", ".mgp", "IANA: MapGuide DBXML",
                        true);
                case "mrc":
                case "application/marc": return ("MARC Formats", "application/marc", ".mrc", "RFC 2220", true);
                case "mrcx":
                case "application/marcxml+xml":
                    return ("MARC21 XML Schema", "application/marcxml+xml", ".mrcx", "RFC 6207", true);
                case "mxf":
                case "application/mxf":
                    return ("Material Exchange Format", "application/mxf", ".mxf", "RFC 4539", true);
                case "nbp":
                case "application/vnd.wolfram.player":
                    return ("Mathematica Notebook Player", "application/vnd.wolfram.player", ".nbp",
                        "IANA: Mathematica Notebook Player", true);
                case "ma":
                case "application/mathematica":
                    return ("Mathematica Notebooks", "application/mathematica", ".ma", "IANA - Mathematica", true);
                case "mathml":
                case "application/mathml+xml":
                    return ("Mathematical Markup Language", "application/mathml+xml", ".mathml", "W3C Math Home", true);
                case "mbox":
                case "application/mbox": return ("Mbox database files", "application/mbox", ".mbox", "RFC 4155", true);
                case "mc1":
                case "application/vnd.medcalcdata":
                    return ("MedCalc", "application/vnd.medcalcdata", ".mc1", "IANA: MedCalc", true);
                case "mscml":
                case "application/mediaservercontrol+xml":
                    return ("Media Server Control Markup Language", "application/mediaservercontrol+xml", ".mscml",
                        "RFC 5022", true);
                case "cdkey":
                case "application/vnd.mediastation.cdkey":
                    return ("MediaRemote", "application/vnd.mediastation.cdkey", ".cdkey", "IANA: MediaRemote", true);
                case "mwf":
                case "application/vnd.mfer":
                    return ("Medical Waveform Encoding Format", "application/vnd.mfer", ".mwf",
                        "IANA: Medical Waveform Encoding Format", true);
                case "mfm":
                case "application/vnd.mfmp":
                    return ("Melody Format for Mobile Platform", "application/vnd.mfmp", ".mfm",
                        "IANA: Melody Format for Mobile Platform", true);
                case "msh":
                case "model/mesh": return ("Mesh Data Type", "model/mesh", ".msh", "RFC 2077", true);
                case "mads":
                case "application/mads+xml":
                    return ("Metadata Authority Description Schema", "application/mads+xml", ".mads", "RFC 6207", true);
                case "mets":
                case "application/mets+xml":
                    return ("Metadata Encoding and Transmission Standard", "application/mets+xml", ".mets", "RFC 6207",
                        true);
                case "mods":
                case "application/mods+xml":
                    return ("Metadata Object Description Schema", "application/mods+xml", ".mods", "RFC 6207", true);
                case "meta4":
                case "application/metalink4+xml":
                    return ("Metalink", "application/metalink4+xml", ".meta4", "Wikipedia: Metalink", true);
                case "mcd":
                case "application/vnd.mcd":
                    return ("Micro CADAM Helix D&D", "application/vnd.mcd", ".mcd", "IANA: Micro CADAM Helix D&D",
                        true);
                case "flo":
                case "application/vnd.micrografx.flo":
                    return ("Micrografx", "application/vnd.micrografx.flo", ".flo", "IANA: Micrografx", true);
                case "igx":
                case "application/vnd.micrografx.igx":
                    return ("Micrografx iGrafx Professional", "application/vnd.micrografx.igx", ".igx",
                        "IANA: Micrografx", true);
                case "es3":
                case "application/vnd.eszigno3+xml":
                    return ("MICROSEC e-Szign¢", "application/vnd.eszigno3+xml", ".es3", "IANA: MICROSEC e-Szign¢",
                        true);
                case "mdb":
                case "application/x-msaccess":
                    return ("Microsoft Access", "application/x-msaccess", ".mdb", "Wikipedia: Microsoft Access", true);
                case "asf":
                case "video/x-ms-asf":
                    return ("Microsoft Advanced Systems Format (ASF);", "video/x-ms-asf", ".asf",
                        "Wikipedia: Advanced Systems Format (ASF);", true);
                case "exe":
                case "application/x-msdownload":
                    return ("Microsoft Application", "application/x-msdownload", ".exe", "Wikipedia: EXE", true);
                case "cil":
                case "application/vnd.ms-artgalry":
                    return ("Microsoft Artgalry", "application/vnd.ms-artgalry", ".cil", "IANA: MS Artgalry", true);
                case "cab":
                case "application/vnd.ms-cab-compressed":
                    return ("Microsoft Cabinet File", "application/vnd.ms-cab-compressed", ".cab",
                        "IANA: MS Cabinet File", true);
                case "ims":
                case "application/vnd.ms-ims":
                    return ("Microsoft Class Server", "application/vnd.ms-ims", ".ims", "IANA: MS Class Server", true);
                case "application":
                case "application/x-ms-application":
                    return ("Microsoft ClickOnce", "application/x-ms-application", ".application",
                        "Wikipedia: ClickOnce", true);
                case "clp":
                case "application/x-msclip":
                    return ("Microsoft Clipboard Clip", "application/x-msclip", ".clp", "Wikipedia: Clipboard", true);
                case "mdi":
                case "image/vnd.ms-modi":
                    return ("Microsoft Document Imaging Format", "image/vnd.ms-modi", ".mdi",
                        "Wikipedia: Microsoft Document Image Format", true);
                case "eot":
                case "application/vnd.ms-fontobject":
                    return ("Microsoft Embedded OpenType", "application/vnd.ms-fontobject", ".eot",
                        "IANA: MS Embedded OpenType", true);
                case "xls":
                case "application/vnd.ms-excel":
                    return ("Microsoft Excel", "application/vnd.ms-excel", ".xls", "IANA: MS Excel", true);
                case "xlam":
                case "application/vnd.ms-excel.addin.macroenabled.12":
                    return ("Microsoft Excel - Add-In File", "application/vnd.ms-excel.addin.macroenabled.12", ".xlam",
                        "IANA: MS Excel", true);
                case "xlsb":
                case "application/vnd.ms-excel.sheet.binary.macroenabled.12":
                    return ("Microsoft Excel - Binary Workbook",
                        "application/vnd.ms-excel.sheet.binary.macroenabled.12", ".xlsb", "IANA: MS Excel", true);
                case "xltm":
                case "application/vnd.ms-excel.template.macroenabled.12":
                    return ("Microsoft Excel - Macro-Enabled Template File",
                        "application/vnd.ms-excel.template.macroenabled.12", ".xltm", "IANA: MS Excel", true);
                case "xlsm":
                case "application/vnd.ms-excel.sheet.macroenabled.12":
                    return ("Microsoft Excel - Macro-Enabled Workbook",
                        "application/vnd.ms-excel.sheet.macroenabled.12", ".xlsm", "IANA: MS Excel", true);
                case "chm":
                case "application/vnd.ms-htmlhelp":
                    return ("Microsoft Html Help File", "application/vnd.ms-htmlhelp", ".chm", "IANA:MS Html Help File",
                        true);
                case "crd":
                case "application/x-mscardfile":
                    return ("Microsoft Information Card", "application/x-mscardfile", ".crd",
                        "Wikipedia: Information Card", true);
                case "lrm":
                case "application/vnd.ms-lrm":
                    return ("Microsoft Learning Resource Module", "application/vnd.ms-lrm", ".lrm",
                        "IANA: MS Learning Resource Module", true);
                case "mvb":
                case "application/x-msmediaview":
                    return ("Microsoft MediaView", "application/x-msmediaview", ".mvb", "Windows Help", true);
                case "mny":
                case "application/x-msmoney":
                    return ("Microsoft Money", "application/x-msmoney", ".mny", "Wikipedia: Microsoft Money", true);
                case "pptx":
                case "application/vnd.openxmlformats-officedocument.presentationml.presentation":
                    return ("Microsoft Office - OOXML - Presentation",
                        "application/vnd.openxmlformats-officedocument.presentationml.presentation", ".pptx",
                        "IANA: OOXML - Presentation", true);
                case "sldx":
                case "application/vnd.openxmlformats-officedocument.presentationml.slide":
                    return ("Microsoft Office - OOXML - Presentation (Slide);",
                        "application/vnd.openxmlformats-officedocument.presentationml.slide", ".sldx",
                        "IANA: OOXML - Presentation", true);
                case "ppsx":
                case "application/vnd.openxmlformats-officedocument.presentationml.slideshow":
                    return ("Microsoft Office - OOXML - Presentation (Slideshow);",
                        "application/vnd.openxmlformats-officedocument.presentationml.slideshow", ".ppsx",
                        "IANA: OOXML - Presentation", true);
                case "potx":
                case "application/vnd.openxmlformats-officedocument.presentationml.template":
                    return ("Microsoft Office - OOXML - Presentation Template",
                        "application/vnd.openxmlformats-officedocument.presentationml.template", ".potx",
                        "IANA: OOXML - Presentation Template", true);
                case "xlsx":
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    return ("Microsoft Office - OOXML - Spreadsheet",
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ".xlsx",
                        "IANA: OOXML - Spreadsheet", true);
                case "xltx":
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.template":
                    return ("Microsoft Office - OOXML - Spreadsheet Template",
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.template", ".xltx",
                        "IANA: OOXML - Spreadsheet Template", true);
                case "docx":
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    return ("Microsoft Office - OOXML - Word Document",
                        "application/vnd.openxmlformats-officedocument.wordprocessingml.document", ".docx",
                        "IANA: OOXML - Word Document", true);
                case "dotx":
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.template":
                    return ("Microsoft Office - OOXML - Word Document Template",
                        "application/vnd.openxmlformats-officedocument.wordprocessingml.template", ".dotx",
                        "IANA: OOXML - Word Document Template", true);
                case "obd":
                case "application/x-msbinder":
                    return ("Microsoft Office Binder", "application/x-msbinder", ".obd",
                        "Wikipedia: Microsoft Shared Tools", true);
                case "thmx":
                case "application/vnd.ms-officetheme":
                    return ("Microsoft Office System Release Theme", "application/vnd.ms-officetheme", ".thmx",
                        "IANA: MS Office System Release Theme", true);
                case "onetoc":
                case "application/onenote":
                    return ("Microsoft OneNote", "application/onenote", ".onetoc", "MS OneNote 2010", true);
                case "pya":
                case "audio/vnd.ms-playready.media.pya":
                    return ("Microsoft PlayReady Ecosystem", "audio/vnd.ms-playready.media.pya", ".pya",
                        "IANA: Microsoft PlayReady Ecosystem", true);
                case "pyv":
                case "video/vnd.ms-playready.media.pyv":
                    return ("Microsoft PlayReady Ecosystem Video", "video/vnd.ms-playready.media.pyv", ".pyv",
                        "IANA: Microsoft PlayReady Ecosystem", true);
                case "ppt":
                case "application/vnd.ms-powerpoint":
                    return ("Microsoft PowerPoint", "application/vnd.ms-powerpoint", ".ppt", "IANA: MS PowerPoint",
                        true);
                case "ppam":
                case "application/vnd.ms-powerpoint.addin.macroenabled.12":
                    return ("Microsoft PowerPoint - Add-in file", "application/vnd.ms-powerpoint.addin.macroenabled.12",
                        ".ppam", "IANA: MS PowerPoint", true);
                case "sldm":
                case "application/vnd.ms-powerpoint.slide.macroenabled.12":
                    return ("Microsoft PowerPoint - Macro-Enabled Open XML Slide",
                        "application/vnd.ms-powerpoint.slide.macroenabled.12", ".sldm", "IANA: MS PowerPoint", true);
                case "pptm":
                case "application/vnd.ms-powerpoint.presentation.macroenabled.12":
                    return ("Microsoft PowerPoint - Macro-Enabled Presentation File",
                        "application/vnd.ms-powerpoint.presentation.macroenabled.12", ".pptm", "IANA: MS PowerPoint",
                        true);
                case "ppsm":
                case "application/vnd.ms-powerpoint.slideshow.macroenabled.12":
                    return ("Microsoft PowerPoint - Macro-Enabled Slide Show File",
                        "application/vnd.ms-powerpoint.slideshow.macroenabled.12", ".ppsm", "IANA: MS PowerPoint",
                        true);
                case "potm":
                case "application/vnd.ms-powerpoint.template.macroenabled.12":
                    return ("Microsoft PowerPoint - Macro-Enabled Template File",
                        "application/vnd.ms-powerpoint.template.macroenabled.12", ".potm", "IANA: MS PowerPoint", true);
                case "mpp":
                case "application/vnd.ms-project":
                    return ("Microsoft Project", "application/vnd.ms-project", ".mpp", "IANA: MS PowerPoint", true);
                case "pub":
                case "application/x-mspublisher":
                    return ("Microsoft Publisher", "application/x-mspublisher", ".pub",
                        "Wikipedia: Microsoft Publisher", true);
                case "scd":
                case "application/x-msschedule":
                    return ("Microsoft Schedule+", "application/x-msschedule", ".scd",
                        "Wikipedia: Microsoft Schedule Plus", true);
                case "xap":
                case "application/x-silverlight-app":
                    return ("Microsoft Silverlight", "application/x-silverlight-app", ".xap", "Wikipedia: Silverlight",
                        true);
                case "stl":
                case "application/vnd.ms-pki.stl":
                    return ("Microsoft Trust UI Provider - Certificate Trust Link", "application/vnd.ms-pki.stl",
                        ".stl", "IANA: MS Trust UI Provider", true);
                case "cat":
                case "application/vnd.ms-pki.seccat":
                    return ("Microsoft Trust UI Provider - Security Catalog", "application/vnd.ms-pki.seccat", ".cat",
                        "IANA: MS Trust UI Provider", true);
                case "vsd":
                case "application/vnd.visio":
                    return ("Microsoft Visio", "application/vnd.visio", ".vsd", "IANA: Visio", true);
                case "vsdx":
                case "application/vnd.visio2013":
                    return ("Microsoft Visio 2013", "application/vnd.visio2013", ".vsdx", "IANA: Visio", true);
                case "wm":
                case "video/x-ms-wm":
                    return ("Microsoft Windows Media", "video/x-ms-wm", ".wm",
                        "Wikipedia: Advanced Systems Format (ASF);", true);
                case "wma":
                case "audio/x-ms-wma":
                    return ("Microsoft Windows Media Audio", "audio/x-ms-wma", ".wma", "Wikipedia: Windows Media Audio",
                        true);
                case "wax":
                case "audio/x-ms-wax":
                    return ("Microsoft Windows Media Audio Redirector", "audio/x-ms-wax", ".wax",
                        "Windows Media Metafiles", true);
                case "wmx":
                case "video/x-ms-wmx":
                    return ("Microsoft Windows Media Audio/Video Playlist", "video/x-ms-wmx", ".wmx",
                        "Wikipedia: Advanced Systems Format (ASF);", true);
                case "wmd":
                case "application/x-ms-wmd":
                    return ("Microsoft Windows Media Player Download Package", "application/x-ms-wmd", ".wmd",
                        "Wikipedia: Windows Media Player", true);
                case "wpl":
                case "application/vnd.ms-wpl":
                    return ("Microsoft Windows Media Player Playlist", "application/vnd.ms-wpl", ".wpl",
                        "IANA: MS Windows Media Player Playlist", true);
                case "wmz":
                case "application/x-ms-wmz":
                    return ("Microsoft Windows Media Player Skin Package", "application/x-ms-wmz", ".wmz",
                        "Wikipedia: Windows Media Player", true);
                case "wmv":
                case "video/x-ms-wmv":
                    return ("Microsoft Windows Media Video", "video/x-ms-wmv", ".wmv",
                        "Wikipedia: Advanced Systems Format (ASF);", true);
                case "wvx":
                case "video/x-ms-wvx":
                    return ("Microsoft Windows Media Video Playlist", "video/x-ms-wvx", ".wvx",
                        "Wikipedia: Advanced Systems Format (ASF);", true);
                case "wmf":
                case "application/x-msmetafile":
                    return ("Microsoft Windows Metafile", "application/x-msmetafile", ".wmf",
                        "Wikipedia: Windows Metafile", true);
                case "trm":
                case "application/x-msterminal":
                    return ("Microsoft Windows Terminal Services", "application/x-msterminal", ".trm",
                        "Wikipedia: Terminal Server", true);
                case "doc":
                case "application/msword":
                    return ("Microsoft Word", "application/msword", ".doc", "Wikipedia: Microsoft Word", true);
                case "docm":
                case "application/vnd.ms-word.document.macroenabled.12":
                    return ("Microsoft Word - Macro-Enabled Document",
                        "application/vnd.ms-word.document.macroenabled.12", ".docm", "IANA: MS Word", true);
                case "dotm":
                case "application/vnd.ms-word.template.macroenabled.12":
                    return ("Microsoft Word - Macro-Enabled Template",
                        "application/vnd.ms-word.template.macroenabled.12", ".dotm", "IANA: MS Word", true);
                case "wri":
                case "application/x-mswrite":
                    return ("Microsoft Wordpad", "application/x-mswrite", ".wri", "Wikipedia: Wordpad", true);
                case "wps":
                case "application/vnd.ms-works":
                    return ("Microsoft Works", "application/vnd.ms-works", ".wps", "IANA: MS Works", true);
                case "xbap":
                case "application/x-ms-xbap":
                    return ("Microsoft XAML Browser Application", "application/x-ms-xbap", ".xbap",
                        "Wikipedia: XAML Browser", true);
                case "xps":
                case "application/vnd.ms-xpsdocument":
                    return ("Microsoft XML Paper Specification", "application/vnd.ms-xpsdocument", ".xps",
                        "IANA: MS XML Paper Specification", true);
                case "mid":
                case "audio/midi":
                    return ("MIDI - Musical Instrument Digital Interface", "audio/midi", ".mid", "Wikipedia: MIDI",
                        true);
                case "mpy":
                case "application/vnd.ibm.minipay":
                    return ("MiniPay", "application/vnd.ibm.minipay", ".mpy", "IANA: MiniPay", true);
                case "afp":
                case "application/vnd.ibm.modcap":
                    return ("MO:DCA-P", "application/vnd.ibm.modcap", ".afp", "IANA: MO:DCA-P", true);
                case "rms":
                case "application/vnd.jcp.javame.midlet-rms":
                    return ("Mobile Information Device Profile", "application/vnd.jcp.javame.midlet-rms", ".rms",
                        "IANA: Mobile Information Device Profile", true);
                case "tmo":
                case "application/vnd.tmobile-livetv":
                    return ("MobileTV", "application/vnd.tmobile-livetv", ".tmo", "IANA: MobileTV", true);
                case "prc":
                case "application/x-mobipocket-ebook":
                    return ("Mobipocket", "application/x-mobipocket-ebook", ".prc", "Wikipedia: Mobipocket", true);
                case "mbk":
                case "application/vnd.mobius.mbk":
                    return ("Mobius Management Systems - Basket file", "application/vnd.mobius.mbk", ".mbk",
                        "IANA: Mobius Management Systems", true);
                case "dis":
                case "application/vnd.mobius.dis":
                    return ("Mobius Management Systems - Distribution Database", "application/vnd.mobius.dis", ".dis",
                        "IANA: Mobius Management Systems", true);
                case "plc":
                case "application/vnd.mobius.plc":
                    return ("Mobius Management Systems - Policy Definition Language File", "application/vnd.mobius.plc",
                        ".plc", "IANA: Mobius Management Systems", true);
                case "mqy":
                case "application/vnd.mobius.mqy":
                    return ("Mobius Management Systems - Query File", "application/vnd.mobius.mqy", ".mqy",
                        "IANA: Mobius Management Systems", true);
                case "msl":
                case "application/vnd.mobius.msl":
                    return ("Mobius Management Systems - Script Language", "application/vnd.mobius.msl", ".msl",
                        "IANA: Mobius Management Systems", true);
                case "txf":
                case "application/vnd.mobius.txf":
                    return ("Mobius Management Systems - Topic Index File", "application/vnd.mobius.txf", ".txf",
                        "IANA: Mobius Management Systems", true);
                case "daf":
                case "application/vnd.mobius.daf":
                    return ("Mobius Management Systems - UniversalArchive", "application/vnd.mobius.daf", ".daf",
                        "IANA: Mobius Management Systems", true);
                case "fly":
                case "text/vnd.fly": return ("mod_fly / fly.cgi", "text/vnd.fly", ".fly", "IANA: Fly", true);
                case "mpc":
                case "application/vnd.mophun.certificate":
                    return ("Mophun Certificate", "application/vnd.mophun.certificate", ".mpc",
                        "IANA: Mophun Certificate", true);
                case "mpn":
                case "application/vnd.mophun.application":
                    return ("Mophun VM", "application/vnd.mophun.application", ".mpn", "IANA: Mophun VM", true);
                case "mj2":
                case "video/mj2": return ("Motion JPEG 2000", "video/mj2", ".mj2", "IANA: MJ2", true);
                case "mpga":
                case "audio/mpeg": return ("MPEG Audio", "audio/mpeg", ".mpga", "Wikipedia: MPGA", true);
                case "mxu":
                case "video/vnd.mpegurl": return ("MPEG Url", "video/vnd.mpegurl", ".mxu", "IANA: MPEG Url", true);
                case "mpeg":
                case "video/mpeg": return ("MPEG Video", "video/mpeg", ".mpeg", "Wikipedia: MPEG", true);
                case "m21":
                case "application/mp21": return ("MPEG-21", "application/mp21", ".m21", "Wikipedia: MPEG-21", true);
                case "mp4a":
                case "audio/mp4": return ("MPEG-4 Audio", "audio/mp4", ".mp4a", "Wikipedia: MP4A", true);
                case "mp4":
                case "video/mp4":
                case "application/mp4": return ("MPEG-4 Video", "video/mp4", ".mp4", "Wikipedia: MP4, RFC 4337", true);
                case "m3u8":
                case "application/vnd.apple.mpegurl":
                    return ("Multimedia Playlist Unicode", "application/vnd.apple.mpegurl", ".m3u8", "Wikipedia: M3U",
                        true);
                case "mus":
                case "application/vnd.musician":
                    return ("MUsical Score Interpreted Code Invented for the ASCII designation of Notation",
                        "application/vnd.musician", ".mus", "IANA: MUSICIAN", true);
                case "msty":
                case "application/vnd.muvee.style":
                    return ("Muvee Automatic Video Editing", "application/vnd.muvee.style", ".msty", "IANA: Muvee",
                        true);
                case "mxml":
                case "application/xv+xml": return ("MXML", "application/xv+xml", ".mxml", "Wikipedia: MXML", true);
                case "ngdat":
                case "application/vnd.nokia.n-gage.data":
                    return ("N-Gage Game Data", "application/vnd.nokia.n-gage.data", ".ngdat", "IANA: N-Gage Game Data",
                        true);
                case "n-gage":
                case "application/vnd.nokia.n-gage.symbian.install":
                    return ("N-Gage Game Installer", "application/vnd.nokia.n-gage.symbian.install", ".n-gage",
                        "IANA: N-Gage Game Installer", true);
                case "ncx":
                case "application/x-dtbncx+xml":
                    return ("Navigation Control file for XML (for ePub);", "application/x-dtbncx+xml", ".ncx",
                        "Wikipedia: EPUB", true);
                case "nc":
                case "application/x-netcdf":
                    return ("Network Common Data Form (NetCDF);", "application/x-netcdf", ".nc", "Wikipedia: NetCDF",
                        true);
                case "nlu":
                case "application/vnd.neurolanguage.nlu":
                    return ("neuroLanguage", "application/vnd.neurolanguage.nlu", ".nlu", "IANA: neuroLanguage", true);
                case "dna":
                case "application/vnd.dna":
                    return ("New Moon Liftoff/DNA", "application/vnd.dna", ".dna", "IANA: New Moon Liftoff/DNA", true);
                case "nnd":
                case "application/vnd.noblenet-directory":
                    return ("NobleNet Directory", "application/vnd.noblenet-directory", ".nnd",
                        "IANA: NobleNet Directory", true);
                case "nns":
                case "application/vnd.noblenet-sealer":
                    return ("NobleNet Sealer", "application/vnd.noblenet-sealer", ".nns", "IANA: NobleNet Sealer",
                        true);
                case "nnw":
                case "application/vnd.noblenet-web":
                    return ("NobleNet Web", "application/vnd.noblenet-web", ".nnw", "IANA: NobleNet Web", true);
                case "rpst":
                case "application/vnd.nokia.radio-preset":
                    return ("Nokia Radio Application - Preset", "application/vnd.nokia.radio-preset", ".rpst",
                        "IANA: Nokia Radio Application", true);
                case "rpss":
                case "application/vnd.nokia.radio-presets":
                    return ("Nokia Radio Application - Preset", "application/vnd.nokia.radio-presets", ".rpss",
                        "IANA: Nokia Radio Application", true);
                case "n3":
                case "text/n3": return ("Notation3", "text/n3", ".n3", "Wikipedia: Notation3", true);
                case "edm":
                case "application/vnd.novadigm.edm":
                    return ("Novadigm's RADIA and EDM products", "application/vnd.novadigm.edm", ".edm",
                        "IANA: Novadigm's RADIA and EDM products", true);
                case "edx":
                case "application/vnd.novadigm.edx":
                    return ("Novadigm's RADIA and EDM products", "application/vnd.novadigm.edx", ".edx",
                        "IANA: Novadigm's RADIA and EDM products", true);
                case "ext":
                case "application/vnd.novadigm.ext":
                    return ("Novadigm's RADIA and EDM products", "application/vnd.novadigm.ext", ".ext",
                        "IANA: Novadigm's RADIA and EDM products", true);
                case "gph":
                case "application/vnd.flographit":
                    return ("NpGraphIt", "application/vnd.flographit", ".gph", "IANA: FloGraphIt", true);
                case "ecelp4800":
                case "audio/vnd.nuera.ecelp4800":
                    return ("Nuera ECELP 4800", "audio/vnd.nuera.ecelp4800", ".ecelp4800", "IANA: ECELP 4800", true);
                case "ecelp7470":
                case "audio/vnd.nuera.ecelp7470":
                    return ("Nuera ECELP 7470", "audio/vnd.nuera.ecelp7470", ".ecelp7470", "IANA: ECELP 7470", true);
                case "ecelp9600":
                case "audio/vnd.nuera.ecelp9600":
                    return ("Nuera ECELP 9600", "audio/vnd.nuera.ecelp9600", ".ecelp9600", "IANA: ECELP 9600", true);
                case "oda":
                case "application/oda":
                    return ("Office Document Architecture", "application/oda", ".oda", "RFC 2161", true);
                case "ogx":
                case "application/ogg": return ("Ogg", "application/ogg", ".ogx", "Wikipedia: Ogg", true);
                case "oga":
                case "audio/ogg": return ("Ogg Audio", "audio/ogg", ".oga", "Wikipedia: Ogg", true);
                case "ogv":
                case "video/ogg": return ("Ogg Video", "video/ogg", ".ogv", "Wikipedia: Ogg", true);
                case "dd2":
                case "application/vnd.oma.dd2+xml":
                    return ("OMA Download Agents", "application/vnd.oma.dd2+xml", ".dd2", "IANA: OMA Download Agents",
                        true);
                case "oth":
                case "application/vnd.oasis.opendocument.text-web":
                    return ("Open Document Text Web", "application/vnd.oasis.opendocument.text-web", ".oth",
                        "IANA: OpenDocument Text Web", true);
                case "opf":
                case "application/oebps-package+xml":
                    return ("Open eBook Publication Structure", "application/oebps-package+xml", ".opf",
                        "Wikipedia: Open eBook", true);
                case "qbo":
                case "application/vnd.intu.qbo":
                    return ("Open Financial Exchange", "application/vnd.intu.qbo", ".qbo",
                        "IANA: Open Financial Exchange", true);
                case "oxt":
                case "application/vnd.openofficeorg.extension":
                    return ("Open Office Extension", "application/vnd.openofficeorg.extension", ".oxt",
                        "IANA: Open Office Extension", true);
                case "osf":
                case "application/vnd.yamaha.openscoreformat":
                    return ("Open Score Format", "application/vnd.yamaha.openscoreformat", ".osf",
                        "IANA: Open Score Format", true);
                case "weba":
                case "audio/webm":
                    return ("Open Web Media Project - Audio", "audio/webm", ".weba", "WebM Project", true);
                case "webm":
                case "video/webm":
                    return ("Open Web Media Project - Video", "video/webm", ".webm", "WebM Project", true);
                case "odc":
                case "application/vnd.oasis.opendocument.chart":
                    return ("OpenDocument Chart", "application/vnd.oasis.opendocument.chart", ".odc",
                        "IANA: OpenDocument Chart", true);
                case "otc":
                case "application/vnd.oasis.opendocument.chart-template":
                    return ("OpenDocument Chart Template", "application/vnd.oasis.opendocument.chart-template", ".otc",
                        "IANA: OpenDocument Chart Template", true);
                case "odb":
                case "application/vnd.oasis.opendocument.database":
                    return ("OpenDocument Database", "application/vnd.oasis.opendocument.database", ".odb",
                        "IANA: OpenDocument Database", true);
                case "odf":
                case "application/vnd.oasis.opendocument.formula":
                    return ("OpenDocument Formula", "application/vnd.oasis.opendocument.formula", ".odf",
                        "IANA: OpenDocument Formula", true);
                case "odft":
                case "application/vnd.oasis.opendocument.formula-template":
                    return ("OpenDocument Formula Template", "application/vnd.oasis.opendocument.formula-template",
                        ".odft", "IANA: OpenDocument Formula Template", true);
                case "odg":
                case "application/vnd.oasis.opendocument.graphics":
                    return ("OpenDocument Graphics", "application/vnd.oasis.opendocument.graphics", ".odg",
                        "IANA: OpenDocument Graphics", true);
                case "otg":
                case "application/vnd.oasis.opendocument.graphics-template":
                    return ("OpenDocument Graphics Template", "application/vnd.oasis.opendocument.graphics-template",
                        ".otg", "IANA: OpenDocument Graphics Template", true);
                case "odi":
                case "application/vnd.oasis.opendocument.image":
                    return ("OpenDocument Image", "application/vnd.oasis.opendocument.image", ".odi",
                        "IANA: OpenDocument Image", true);
                case "oti":
                case "application/vnd.oasis.opendocument.image-template":
                    return ("OpenDocument Image Template", "application/vnd.oasis.opendocument.image-template", ".oti",
                        "IANA: OpenDocument Image Template", true);
                case "odp":
                case "application/vnd.oasis.opendocument.presentation":
                    return ("OpenDocument Presentation", "application/vnd.oasis.opendocument.presentation", ".odp",
                        "IANA: OpenDocument Presentation", true);
                case "otp":
                case "application/vnd.oasis.opendocument.presentation-template":
                    return ("OpenDocument Presentation Template",
                        "application/vnd.oasis.opendocument.presentation-template", ".otp",
                        "IANA: OpenDocument Presentation Template", true);
                case "ods":
                case "application/vnd.oasis.opendocument.spreadsheet":
                    return ("OpenDocument Spreadsheet", "application/vnd.oasis.opendocument.spreadsheet", ".ods",
                        "IANA: OpenDocument Spreadsheet", true);
                case "ots":
                case "application/vnd.oasis.opendocument.spreadsheet-template":
                    return ("OpenDocument Spreadsheet Template",
                        "application/vnd.oasis.opendocument.spreadsheet-template", ".ots",
                        "IANA: OpenDocument Spreadsheet Template", true);
                case "odt":
                case "application/vnd.oasis.opendocument.text":
                    return ("OpenDocument Text", "application/vnd.oasis.opendocument.text", ".odt",
                        "IANA: OpenDocument Text", true);
                case "odm":
                case "application/vnd.oasis.opendocument.text-master":
                    return ("OpenDocument Text Master", "application/vnd.oasis.opendocument.text-master", ".odm",
                        "IANA: OpenDocument Text Master", true);
                case "ott":
                case "application/vnd.oasis.opendocument.text-template":
                    return ("OpenDocument Text Template", "application/vnd.oasis.opendocument.text-template", ".ott",
                        "IANA: OpenDocument Text Template", true);
                case "ktx":
                case "image/ktx": return ("OpenGL Textures (KTX);", "image/ktx", ".ktx", "KTX File Format", true);
                case "sxc":
                case "application/vnd.sun.xml.calc":
                    return ("OpenOffice - Calc (Spreadsheet);", "application/vnd.sun.xml.calc", ".sxc",
                        "Wikipedia: OpenOffice", true);
                case "stc":
                case "application/vnd.sun.xml.calc.template":
                    return ("OpenOffice - Calc Template (Spreadsheet);", "application/vnd.sun.xml.calc.template",
                        ".stc", "Wikipedia: OpenOffice", true);
                case "sxd":
                case "application/vnd.sun.xml.draw":
                    return ("OpenOffice - Draw (Graphics);", "application/vnd.sun.xml.draw", ".sxd",
                        "Wikipedia: OpenOffice", true);
                case "std":
                case "application/vnd.sun.xml.draw.template":
                    return ("OpenOffice - Draw Template (Graphics);", "application/vnd.sun.xml.draw.template", ".std",
                        "Wikipedia: OpenOffice", true);
                case "sxi":
                case "application/vnd.sun.xml.impress":
                    return ("OpenOffice - Impress (Presentation);", "application/vnd.sun.xml.impress", ".sxi",
                        "Wikipedia: OpenOffice", true);
                case "sti":
                case "application/vnd.sun.xml.impress.template":
                    return ("OpenOffice - Impress Template (Presentation);", "application/vnd.sun.xml.impress.template",
                        ".sti", "Wikipedia: OpenOffice", true);
                case "sxm":
                case "application/vnd.sun.xml.math":
                    return ("OpenOffice - Math (Formula);", "application/vnd.sun.xml.math", ".sxm",
                        "Wikipedia: OpenOffice", true);
                case "sxw":
                case "application/vnd.sun.xml.writer":
                    return ("OpenOffice - Writer (Text - HTML);", "application/vnd.sun.xml.writer", ".sxw",
                        "Wikipedia: OpenOffice", true);
                case "sxg":
                case "application/vnd.sun.xml.writer.global":
                    return ("OpenOffice - Writer (Text - HTML);", "application/vnd.sun.xml.writer.global", ".sxg",
                        "Wikipedia: OpenOffice", true);
                case "stw":
                case "application/vnd.sun.xml.writer.template":
                    return ("OpenOffice - Writer Template (Text - HTML);", "application/vnd.sun.xml.writer.template",
                        ".stw", "Wikipedia: OpenOffice", true);
                case "otf":
                case "application/x-font-otf":
                    return ("OpenType Font File", "application/x-font-otf", ".otf", "OpenType Font File", true);
                case "osfpvg":
                case "application/vnd.yamaha.openscoreformat.osfpvg+xml":
                    return ("OSFPVG", "application/vnd.yamaha.openscoreformat.osfpvg+xml", ".osfpvg", "IANA: OSFPVG",
                        true);
                case "dp":
                case "application/vnd.osgi.dp":
                    return ("OSGi Deployment Package", "application/vnd.osgi.dp", ".dp",
                        "IANA: OSGi Deployment Package", true);
                case "pdb":
                case "application/vnd.palm":
                    return ("PalmOS Data", "application/vnd.palm", ".pdb", "IANA: PalmOS Data", true);
                case "p":
                case "text/x-pascal": return ("Pascal Source File", "text/x-pascal", ".p", "Wikipedia: Pascal", true);
                case "paw":
                case "application/vnd.pawaafile":
                    return ("PawaaFILE", "application/vnd.pawaafile", ".paw", "IANA: PawaaFILE", true);
                case "pclxl":
                case "application/vnd.hp-pclxl":
                    return ("PCL 6 Enhanced (Formely PCL XL);", "application/vnd.hp-pclxl", ".pclxl", "IANA: HP PCL XL",
                        true);
                case "efif":
                case "application/vnd.picsel":
                    return ("Pcsel eFIF File", "application/vnd.picsel", ".efif", "IANA: Picsel eFIF File", true);
                case "pcx":
                case "image/x-pcx": return ("PCX Image", "image/x-pcx", ".pcx", "Wikipedia: PCX", true);
                case "psd":
                case "image/vnd.adobe.photoshop":
                    return ("Photoshop Document", "image/vnd.adobe.photoshop", ".psd", "Wikipedia: Photoshop Document",
                        true);
                case "prf":
                case "application/pics-rules":
                    return ("PICSRules", "application/pics-rules", ".prf", "W3C PICSRules", true);
                case "pic":
                case "image/x-pict": return ("PICT Image", "image/x-pict", ".pic", "Wikipedia: PICT", true);
                case "chat":
                case "application/x-chat": return ("pIRCh", "application/x-chat", ".chat", "Wikipedia: pIRCh", true);
                case "p10":
                case "application/pkcs10":
                    return ("PKCS #10 - Certification Request Standard", "application/pkcs10", ".p10", "RFC 2986",
                        true);
                case "p12":
                case "application/x-pkcs12":
                    return ("PKCS #12 - Personal Information Exchange Syntax Standard", "application/x-pkcs12", ".p12",
                        "RFC 2986", true);
                case "p7m":
                case "application/pkcs7-mime":
                    return ("PKCS #7 - Cryptographic Message Syntax Standard", "application/pkcs7-mime", ".p7m",
                        "RFC 2315", true);
                case "p7s":
                case "application/pkcs7-signature":
                    return ("PKCS #7 - Cryptographic Message Syntax Standard", "application/pkcs7-signature", ".p7s",
                        "RFC 2315", true);
                case "p7r":
                case "application/x-pkcs7-certreqresp":
                    return ("PKCS #7 - Cryptographic Message Syntax Standard (Certificate Request Response);",
                        "application/x-pkcs7-certreqresp", ".p7r", "RFC 2986", true);
                case "p7b":
                case "application/x-pkcs7-certificates":
                    return ("PKCS #7 - Cryptographic Message Syntax Standard (Certificates);",
                        "application/x-pkcs7-certificates", ".p7b", "RFC 2986", true);
                case "p8":
                case "application/pkcs8":
                    return ("PKCS #8 - Private-Key Information Syntax Standard", "application/pkcs8", ".p8", "RFC 5208",
                        true);
                case "plf":
                case "application/vnd.pocketlearn":
                    return ("PocketLearn Viewers", "application/vnd.pocketlearn", ".plf", "IANA: PocketLearn Viewers",
                        true);
                case "pnm":
                case "image/x-portable-anymap":
                    return ("Portable Anymap Image", "image/x-portable-anymap", ".pnm", "Wikipedia: Netpbm Format",
                        true);
                case "pbm":
                case "image/x-portable-bitmap":
                    return ("Portable Bitmap Format", "image/x-portable-bitmap", ".pbm", "Wikipedia: Netpbm Format",
                        true);
                case "pcf":
                case "application/x-font-pcf":
                    return ("Portable Compiled Format", "application/x-font-pcf", ".pcf",
                        "Wikipedia: Portable Compiled Format", true);
                case "pfr":
                case "application/font-tdpfr":
                    return ("Portable Font Resource", "application/font-tdpfr", ".pfr", "RFC 3073", true);
                case "pgn":
                case "application/x-chess-pgn":
                    return ("Portable Game Notation (Chess Games);", "application/x-chess-pgn", ".pgn",
                        "Wikipedia: Portable Game Notationb", true);
                case "pgm":
                case "image/x-portable-graymap":
                    return ("Portable Graymap Format", "image/x-portable-graymap", ".pgm", "Wikipedia: Netpbm Format",
                        true);
                case "png":
                case "image/png":
                case "image/x-citrix-png":
                case "image/x-png":
                    return ("Portable Network Graphics (PNG);", "image/png", ".png", "RFC 2083, RFC 2083", false);

                case "ppm":
                case "image/x-portable-pixmap":
                    return ("Portable Pixmap Format", "image/x-portable-pixmap", ".ppm", "Wikipedia: Netpbm Format",
                        true);
                case "pskcxml":
                case "application/pskc+xml":
                    return ("Portable Symmetric Key Container", "application/pskc+xml", ".pskcxml", "RFC 6030", true);
                case "pml":
                case "application/vnd.ctc-posml":
                    return ("PosML", "application/vnd.ctc-posml", ".pml", "IANA: PosML", true);
                case "ai":
                case "application/postscript":
                    return ("PostScript", "application/postscript", ".ai", "Wikipedia: PostScript", true);
                case "pfa":
                case "application/x-font-type1":
                    return ("PostScript Fonts", "application/x-font-type1", ".pfa", "Wikipedia: PostScript Fonts",
                        true);
                case "pbd":
                case "application/vnd.powerbuilder6":
                    return ("PowerBuilder", "application/vnd.powerbuilder6", ".pbd", "IANA: PowerBuilder", true);
                case "pgp":
                case "application/pgp-encrypted":
                case "application/pgp-signature":
                    return ("Pretty Good Privacy", "application/pgp-encrypted,application/pgp-signature", ".pgp",
                        "RFC 2015", true);
                case "box":
                case "application/vnd.previewsystems.box":
                    return ("Preview Systems ZipLock/VBox", "application/vnd.previewsystems.box", ".box",
                        "IANA: Preview Systems ZipLock/Vbox", true);
                case "ptid":
                case "application/vnd.pvi.ptid1":
                    return ("Princeton Video Image", "application/vnd.pvi.ptid1", ".ptid",
                        "IANA: Princeton Video Image", true);
                case "pls":
                case "application/pls+xml":
                    return ("Pronunciation Lexicon Specification", "application/pls+xml", ".pls", "RFC 4267", true);
                case "str":
                case "application/vnd.pg.format":
                    return ("Proprietary P&G Standard Reporting System", "application/vnd.pg.format", ".str",
                        "IANA: Proprietary P&G Standard Reporting System", true);
                case "ei6":
                case "application/vnd.pg.osasli":
                    return ("Proprietary P&G Standard Reporting System", "application/vnd.pg.osasli", ".ei6",
                        "IANA: Proprietary P&G Standard Reporting System", true);
                case "dsc":
                case "text/prs.lines.tag":
                    return ("PRS Lines Tag", "text/prs.lines.tag", ".dsc", "IANA: PRS Lines Tag", true);
                case "psf":
                case "application/x-font-linux-psf":
                    return ("PSF Fonts", "application/x-font-linux-psf", ".psf", "PSF Fonts", true);
                case "qps":
                case "application/vnd.publishare-delta-tree":
                    return ("PubliShare Objects", "application/vnd.publishare-delta-tree", ".qps",
                        "IANA: PubliShare Objects", true);
                case "wg":
                case "application/vnd.pmi.widget":
                    return ("Qualcomm's Plaza Mobile Internet", "application/vnd.pmi.widget", ".wg",
                        "IANA: Qualcomm's Plaza Mobile Internet", true);
                case "qxd":
                case "application/vnd.quark.quarkxpress":
                    return ("QuarkXpress", "application/vnd.quark.quarkxpress", ".qxd", "IANA: QuarkXPress", true);
                case "esf":
                case "application/vnd.epson.esf":
                    return ("QUASS Stream Player", "application/vnd.epson.esf", ".esf", "IANA: QUASS Stream Player",
                        true);
                case "msf":
                case "application/vnd.epson.msf":
                    return ("QUASS Stream Player", "application/vnd.epson.msf", ".msf", "IANA: QUASS Stream Player",
                        true);
                case "ssf":
                case "application/vnd.epson.ssf":
                    return ("QUASS Stream Player", "application/vnd.epson.ssf", ".ssf", "IANA: QUASS Stream Player",
                        true);
                case "qam":
                case "application/vnd.epson.quickanime":
                    return ("QuickAnime Player", "application/vnd.epson.quickanime", ".qam", "IANA: QuickAnime Player",
                        true);
                case "qfx":
                case "application/vnd.intu.qfx":
                    return ("Quicken", "application/vnd.intu.qfx", ".qfx", "IANA: Quicken", true);
                case "qt":
                case "video/quicktime":
                    return ("Quicktime Video", "video/quicktime", ".qt", "Wikipedia: Quicktime", true);
                case "rar":
                case "application/x-rar-compressed":
                    return ("RAR Archive", "application/x-rar-compressed", ".rar", "Wikipedia: RAR", true);
                case "ram":
                case "audio/x-pn-realaudio":
                    return ("Real Audio Sound", "audio/x-pn-realaudio", ".ram", "Wikipedia: RealPlayer", true);
                case "rmp":
                case "audio/x-pn-realaudio-plugin":
                    return ("Real Audio Sound", "audio/x-pn-realaudio-plugin", ".rmp", "Wikipedia: RealPlayer", true);
                case "rsd":
                case "application/rsd+xml":
                    return ("Really Simple Discovery", "application/rsd+xml", ".rsd",
                        "Wikipedia: Really Simple Discovery", true);
                case "rm":
                case "application/vnd.rn-realmedia":
                    return ("RealMedia", "application/vnd.rn-realmedia", ".rm", null, true);

                case "bed":
                case "application/vnd.realvnc.bed":
                    return ("RealVNC", "application/vnd.realvnc.bed", ".bed", "IANA: RealVNC", true);
                case "mxl":
                case "application/vnd.recordare.musicxml":
                    return ("Recordare Applications", "application/vnd.recordare.musicxml", ".mxl",
                        "IANA: Recordare Apps", true);
                case "musicxml":
                case "application/vnd.recordare.musicxml+xml":
                    return ("Recordare Applications", "application/vnd.recordare.musicxml+xml", ".musicxml",
                        "IANA: Recordare Apps", true);
                case "rnc":
                case "application/relax-ng-compact-syntax":
                    return ("Relax NG Compact Syntax", "application/relax-ng-compact-syntax", ".rnc", "Relax NG", true);
                case "rdz":
                case "application/vnd.data-vision.rdz":
                    return ("RemoteDocs R-Viewer", "application/vnd.data-vision.rdz", ".rdz", "IANA: Data-Vision",
                        true);
                case "rdf":
                case "application/rdf+xml":
                    return ("Resource Description Framework", "application/rdf+xml", ".rdf", "RFC 3870", true);
                case "rp9":
                case "application/vnd.cloanto.rp9":
                    return ("RetroPlatform Player", "application/vnd.cloanto.rp9", ".rp9", "IANA: RetroPlatform Player",
                        true);
                case "jisp":
                case "application/vnd.jisp": return ("RhymBox", "application/vnd.jisp", ".jisp", "IANA: RhymBox", true);
                case "rtf":
                case "application/rtf":
                    return ("Rich Text Format", "application/rtf", ".rtf", "Wikipedia: Rich Text Format", true);
                case "rtx":
                case "text/richtext":
                    return ("Rich Text Format (RTF);", "text/richtext", ".rtx", "Wikipedia: Rich Text Format", true);
                case "link66":
                case "application/vnd.route66.link66+xml":
                    return ("ROUTE 66 Location Based Services", "application/vnd.route66.link66+xml", ".link66",
                        "IANA: ROUTE 66", true);
                case "rss, .xml":
                case "application/rss+xml":
                    return ("RSS - Really Simple Syndication", "application/rss+xml", ".rss, .xml", "Wikipedia: RSS",
                        true);
                case "shf":
                case "application/shf+xml":
                    return ("S Hexdump Format", "application/shf+xml", ".shf", "RFC 4194", true);
                case "st":
                case "application/vnd.sailingtracker.track":
                    return ("SailingTracker", "application/vnd.sailingtracker.track", ".st", "IANA: SailingTracker",
                        true);
                case "svg":
                case "image/svg+xml":
                    return ("Scalable Vector Graphics (SVG);", "image/svg+xml", ".svg", "Wikipedia: SVG", true);
                case "sus":
                case "application/vnd.sus-calendar":
                    return ("ScheduleUs", "application/vnd.sus-calendar", ".sus", "IANA: ScheduleUs", true);
                case "sru":
                case "application/sru+xml":
                    return ("Search/Retrieve via URL Response Format", "application/sru+xml", ".sru", "RFC 6207", true);
                case "setpay":
                case "application/set-payment-initiation":
                    return ("Secure Electronic Transaction - Payment", "application/set-payment-initiation", ".setpay",
                        "IANA: SET Payment", true);
                case "setreg":
                case "application/set-registration-initiation":
                    return ("Secure Electronic Transaction - Registration", "application/set-registration-initiation",
                        ".setreg", "IANA: SET Registration", true);
                case "sema":
                case "application/vnd.sema":
                    return ("Secured eMail", "application/vnd.sema", ".sema", "IANA: Secured eMail", true);
                case "semd":
                case "application/vnd.semd":
                    return ("Secured eMail", "application/vnd.semd", ".semd", "IANA: Secured eMail", true);
                case "semf":
                case "application/vnd.semf":
                    return ("Secured eMail", "application/vnd.semf", ".semf", "IANA: Secured eMail", true);
                case "see":
                case "application/vnd.seemail":
                    return ("SeeMail", "application/vnd.seemail", ".see", "IANA: SeeMail", true);
                case "snf":
                case "application/x-font-snf":
                    return ("Server Normal Format", "application/x-font-snf", ".snf", "Wikipedia: Server Normal Format",
                        true);
                case "spq":
                case "application/scvp-vp-request":
                    return ("Server-Based Certificate Validation Protocol - Validation Policies - Request",
                        "application/scvp-vp-request", ".spq", "RFC 5055", true);
                case "spp":
                case "application/scvp-vp-response":
                    return ("Server-Based Certificate Validation Protocol - Validation Policies - Response",
                        "application/scvp-vp-response", ".spp", "RFC 5055", true);
                case "scq":
                case "application/scvp-cv-request":
                    return ("Server-Based Certificate Validation Protocol - Validation Request",
                        "application/scvp-cv-request", ".scq", "RFC 5055", true);
                case "scs":
                case "application/scvp-cv-response":
                    return ("Server-Based Certificate Validation Protocol - Validation Response",
                        "application/scvp-cv-response", ".scs", "RFC 5055", true);
                case "sdp":
                case "application/sdp":
                    return ("Session Description Protocol", "application/sdp", ".sdp", "RFC 2327", true);
                case "etx":
                case "text/x-setext": return ("Setext", "text/x-setext", ".etx", "Wikipedia: Setext", true);
                case "movie":
                case "video/x-sgi-movie": return ("SGI Movie", "video/x-sgi-movie", ".movie", "SGI Facts", true);
                case "ifm":
                case "application/vnd.shana.informed.formdata":
                    return ("Shana Informed Filler", "application/vnd.shana.informed.formdata", ".ifm",
                        "IANA: Shana Informed Filler", true);
                case "itp":
                case "application/vnd.shana.informed.formtemplate":
                    return ("Shana Informed Filler", "application/vnd.shana.informed.formtemplate", ".itp",
                        "IANA: Shana Informed Filler", true);
                case "iif":
                case "application/vnd.shana.informed.interchange":
                    return ("Shana Informed Filler", "application/vnd.shana.informed.interchange", ".iif",
                        "IANA: Shana Informed Filler", true);
                case "ipk":
                case "application/vnd.shana.informed.package":
                    return ("Shana Informed Filler", "application/vnd.shana.informed.package", ".ipk",
                        "IANA: Shana Informed Filler", true);
                case "tfi":
                case "application/thraud+xml":
                    return ("Sharing Transaction Fraud Data", "application/thraud+xml", ".tfi", "RFC 5941", true);
                case "shar":
                case "application/x-shar":
                    return ("Shell Archive", "application/x-shar", ".shar", "Wikipedia: Shell Archie", true);
                case "rgb":
                case "image/x-rgb":
                    return ("Silicon Graphics RGB Bitmap", "image/x-rgb", ".rgb", "RGB Image Format", true);
                case "slt":
                case "application/vnd.epson.salt":
                    return ("SimpleAnimeLite Player", "application/vnd.epson.salt", ".slt",
                        "IANA: SimpleAnimeLite Player", true);
                case "aso":
                case "application/vnd.accpac.simply.aso":
                    return ("Simply Accounting", "application/vnd.accpac.simply.aso", ".aso", "IANA: Simply Accounting",
                        true);
                case "imp":
                case "application/vnd.accpac.simply.imp":
                    return ("Simply Accounting - Data Import", "application/vnd.accpac.simply.imp", ".imp",
                        "IANA: Simply Accounting", true);
                case "twd":
                case "application/vnd.simtech-mindmapper":
                    return ("SimTech MindMapper", "application/vnd.simtech-mindmapper", ".twd",
                        "IANA: SimTech MindMapper", true);
                case "csp":
                case "application/vnd.commonspace":
                    return ("Sixth Floor Media - CommonSpace", "application/vnd.commonspace", ".csp",
                        "IANA: CommonSpace", true);
                case "saf":
                case "application/vnd.yamaha.smaf-audio":
                    return ("SMAF Audio", "application/vnd.yamaha.smaf-audio", ".saf", "IANA: SMAF Audio", true);
                case "mmf":
                case "application/vnd.smaf":
                    return ("SMAF File", "application/vnd.smaf", ".mmf", "IANA: SMAF File", true);
                case "spf":
                case "application/vnd.yamaha.smaf-phrase":
                    return ("SMAF Phrase", "application/vnd.yamaha.smaf-phrase", ".spf", "IANA: SMAF Phrase", true);
                case "teacher":
                case "application/vnd.smart.teacher":
                    return ("SMART Technologies Apps", "application/vnd.smart.teacher", ".teacher",
                        "IANA: SMART Technologies Apps", true);
                case "svd":
                case "application/vnd.svd":
                    return ("SourceView Document", "application/vnd.svd", ".svd", "IANA: SourceView Document", true);
                case "rq":
                case "application/sparql-query":
                    return ("SPARQL - Query", "application/sparql-query", ".rq", "W3C SPARQL", true);
                case "srx":
                case "application/sparql-results+xml":
                    return ("SPARQL - Results", "application/sparql-results+xml", ".srx", "W3C SPARQL", true);
                case "gram":
                case "application/srgs":
                    return ("Speech Recognition Grammar Specification", "application/srgs", ".gram",
                        "W3C Speech Grammar", true);
                case "grxml":
                case "application/srgs+xml":
                    return ("Speech Recognition Grammar Specification - XML", "application/srgs+xml", ".grxml",
                        "W3C Speech Grammar", true);
                case "ssml":
                case "application/ssml+xml":
                    return ("Speech Synthesis Markup Language", "application/ssml+xml", ".ssml", "W3C Speech Synthesis",
                        true);
                case "skp":
                case "application/vnd.koan":
                    return ("SSEYO Koan Play File", "application/vnd.koan", ".skp", "IANA: SSEYO Koan Play File", true);
                case "sgml":
                case "text/sgml":
                    return ("Standard Generalized Markup Language (SGML);", "text/sgml", ".sgml", "Wikipedia: SGML",
                        true);
                case "sdc":
                case "application/vnd.stardivision.calc":
                    return ("StarOffice - Calc", "application/vnd.stardivision.calc", ".sdc", null, true);

                case "sdd":
                case "application/vnd.stardivision.impress":
                    return ("StarOffice - Impress", "application/vnd.stardivision.impress", ".sdd", null, true);

                case "smf":
                case "application/vnd.stardivision.math":
                    return ("StarOffice - Math", "application/vnd.stardivision.math", ".smf", null, true);

                case "sdw":
                case "application/vnd.stardivision.writer":
                    return ("StarOffice - Writer", "application/vnd.stardivision.writer", ".sdw", null, true);

                case "sgl":
                case "application/vnd.stardivision.writer-global":
                    return ("StarOffice - Writer (Global);", "application/vnd.stardivision.writer-global", ".sgl", null,
                        true);

                case "sm":
                case "application/vnd.stepmania.stepchart":
                    return ("StepMania", "application/vnd.stepmania.stepchart", ".sm", "IANA: StepMania", true);
                case "sda":
                case "application/vnd.stardivision.draw":
                    return ("StarOffice - Draw", "application/vnd.stardivision.draw", ".sda", null, true);

                case "sit":
                case "application/x-stuffit":
                    return ("Stuffit Archive", "application/x-stuffit", ".sit", "Wikipedia: Stuffit", true);
                case "sitx":
                case "application/x-stuffitx":
                    return ("Stuffit Archive", "application/x-stuffitx", ".sitx", "Wikipedia: Stuffit", true);
                case "sdkm":
                case "application/vnd.solent.sdkm+xml":
                    return ("SudokuMagic", "application/vnd.solent.sdkm+xml", ".sdkm", "IANA: SudokuMagic", true);
                case "xo":
                case "application/vnd.olpc-sugar":
                    return ("Sugar Linux Application Bundle", "application/vnd.olpc-sugar", ".xo",
                        "IANA: Sugar Linux App Bundle", true);
                case "au":
                case "audio/basic":
                    return ("Sun Audio - Au file format", "audio/basic", ".au", "Wikipedia: Sun audio", true);
                case "wqd":
                case "application/vnd.wqd":
                    return ("SundaHus WQ", "application/vnd.wqd", ".wqd", "IANA: SundaHus WQ", true);
                case "sis":
                case "application/vnd.symbian.install":
                    return ("Symbian Install Package", "application/vnd.symbian.install", ".sis",
                        "IANA: Symbian Install", true);
                case "smi":
                case "application/smil+xml":
                    return ("Synchronized Multimedia Integration Language", "application/smil+xml", ".smi", "RFC 4536",
                        true);
                case "xsm":
                case "application/vnd.syncml+xml":
                    return ("SyncML", "application/vnd.syncml+xml", ".xsm", "IANA: SyncML", true);
                case "bdm":
                case "application/vnd.syncml.dm+wbxml":
                    return ("SyncML - Device Management", "application/vnd.syncml.dm+wbxml", ".bdm", "IANA: SyncML",
                        true);
                case "xdm":
                case "application/vnd.syncml.dm+xml":
                    return ("SyncML - Device Management", "application/vnd.syncml.dm+xml", ".xdm", "IANA: SyncML",
                        true);
                case "sv4cpio":
                case "application/x-sv4cpio":
                    return ("System V Release 4 CPIO Archive", "application/x-sv4cpio", ".sv4cpio", "Wikipedia: pax",
                        true);
                case "sv4crc":
                case "application/x-sv4crc":
                    return ("System V Release 4 CPIO Checksum Data", "application/x-sv4crc", ".sv4crc",
                        "Wikipedia: pax", true);
                case "sbml":
                case "application/sbml+xml":
                    return ("Systems Biology Markup Language", "application/sbml+xml", ".sbml", "RFC 3823", true);
                case "tsv":
                case "text/tab-separated-values":
                    return ("Tab Seperated Values", "text/tab-separated-values", ".tsv", "Wikipedia: TSV", true);
                case "tiff":
                case "image/tiff": return ("Tagged Image File Format", "image/tiff", ".tiff", "Wikipedia: TIFF", true);
                case "tao":
                case "application/vnd.tao.intent-module-archive":
                    return ("Tao Intent", "application/vnd.tao.intent-module-archive", ".tao", "IANA: Tao Intent",
                        true);
                case "tar":
                case "application/x-tar":
                    return ("Tar File (Tape Archive);", "application/x-tar", ".tar", "Wikipedia: Tar", true);
                case "tcl":
                case "application/x-tcl": return ("Tcl Script", "application/x-tcl", ".tcl", "Wikipedia: Tcl", true);
                case "tex":
                case "application/x-tex": return ("TeX", "application/x-tex", ".tex", "Wikipedia: TeX", true);
                case "tfm":
                case "application/x-tex-tfm":
                    return ("TeX Font Metric", "application/x-tex-tfm", ".tfm", "Wikipedia: TeX Font Metric", true);
                case "tei":
                case "application/tei+xml":
                    return ("Text Encoding and Interchange", "application/tei+xml", ".tei", "RFC 6129", true);
                case "txt":
                case "text/plain": return ("Text File", "text/plain", ".txt", "Wikipedia: Text File", true);
                case "dxp":
                case "application/vnd.spotfire.dxp":
                    return ("TIBCO Spotfire", "application/vnd.spotfire.dxp", ".dxp", "IANA: TIBCO Spotfire", true);
                case "sfs":
                case "application/vnd.spotfire.sfs":
                    return ("TIBCO Spotfire", "application/vnd.spotfire.sfs", ".sfs", "IANA: TIBCO Spotfire", true);
                case "tsd":
                case "application/timestamped-data":
                    return ("Time Stamped Data Envelope", "application/timestamped-data", ".tsd", "RFC 5955", true);
                case "tpt":
                case "application/vnd.trid.tpt":
                    return ("TRI Systems Config", "application/vnd.trid.tpt", ".tpt", "IANA: TRI Systems", true);
                case "mxs":
                case "application/vnd.triscape.mxs":
                    return ("Triscape Map Explorer", "application/vnd.triscape.mxs", ".mxs",
                        "IANA: Triscape Map Explorer", true);
                case "t":
                case "text/troff": return ("troff", "text/troff", ".t", "Wikipedia: troff", true);
                case "tra":
                case "application/vnd.trueapp":
                    return ("True BASIC", "application/vnd.trueapp", ".tra", "IANA: True BASIC", true);
                case "ttf":
                case "application/x-font-ttf":
                    return ("TrueType Font", "application/x-font-ttf", ".ttf", "Wikipedia: TrueType", true);
                case "ttl":
                case "text/turtle":
                    return ("Turtle (Terse RDF Triple Language);", "text/turtle", ".ttl", "Wikipedia: Turtle", true);
                case "umj":
                case "application/vnd.umajin":
                    return ("UMAJIN", "application/vnd.umajin", ".umj", "IANA: UMAJIN", true);
                case "uoml":
                case "application/vnd.uoml+xml":
                    return ("Unique Object Markup Language", "application/vnd.uoml+xml", ".uoml", "IANA: UOML", true);
                case "unityweb":
                case "application/vnd.unity":
                    return ("Unity 3d", "application/vnd.unity", ".unityweb", "IANA: Unity 3d", true);
                case "ufd":
                case "application/vnd.ufdl":
                    return ("Universal Forms Description Language", "application/vnd.ufdl", ".ufd",
                        "IANA: Universal Forms Description Language", true);
                case "uri":
                case "text/uri-list": return ("URI Resolution Services", "text/uri-list", ".uri", "RFC 2483", true);
                case "utz":
                case "application/vnd.uiq.theme":
                    return ("User Interface Quartz - Theme (Symbian);", "application/vnd.uiq.theme", ".utz",
                        "IANA: User Interface Quartz", true);
                case "ustar":
                case "application/x-ustar":
                    return ("Ustar (Uniform Standard Tape Archive);", "application/x-ustar", ".ustar",
                        "Wikipedia: Ustar", true);
                case "uu":
                case "text/x-uuencode": return ("UUEncode", "text/x-uuencode", ".uu", "Wikipedia: UUEncode", true);
                case "vcs":
                case "text/x-vcalendar": return ("vCalendar", "text/x-vcalendar", ".vcs", "Wikipedia: vCalendar", true);
                case "vcf":
                case "text/x-vcard": return ("vCard", "text/x-vcard", ".vcf", "Wikipedia: vCard", true);
                case "vcd":
                case "application/x-cdlink":
                    return ("Video CD", "application/x-cdlink", ".vcd", "Wikipedia: Video CD", true);
                case "vsf":
                case "application/vnd.vsf":
                    return ("Viewport+", "application/vnd.vsf", ".vsf", "IANA: Viewport+", true);
                case "wrl":
                case "model/vrml":
                    return ("Virtual Reality Modeling Language", "model/vrml", ".wrl", "Wikipedia: VRML", true);
                case "vcx":
                case "application/vnd.vcx":
                    return ("VirtualCatalog", "application/vnd.vcx", ".vcx", "IANA: VirtualCatalog", true);
                case "mts":
                case "model/vnd.mts": return ("Virtue MTS", "model/vnd.mts", ".mts", "IANA: MTS", true);
                case "vtu":
                case "model/vnd.vtu": return ("Virtue VTU", "model/vnd.vtu", ".vtu", "IANA: VTU", true);
                case "vis":
                case "application/vnd.visionary":
                    return ("Visionary", "application/vnd.visionary", ".vis", "IANA: Visionary", true);
                case "viv":
                case "video/vnd.vivo": return ("Vivo", "video/vnd.vivo", ".viv", "IANA: Vivo", true);
                case "ccxml":
                case "application/ccxml+xml,":
                    return ("Voice Browser Call Control", "application/ccxml+xml,", ".ccxml",
                        "Voice Browser Call Control: CCXML Version 1.0", true);
                case "vxml":
                case "application/voicexml+xml":
                    return ("VoiceXML", "application/voicexml+xml", ".vxml", "RFC 4267", true);
                case "src":
                case "application/x-wais-source":
                    return ("WAIS Source", "application/x-wais-source", ".src", "YoLinux", true);
                case "wbxml":
                case "application/vnd.wap.wbxml":
                    return ("WAP Binary XML (WBXML);", "application/vnd.wap.wbxml", ".wbxml", "IANA: WBXML", true);
                case "wbmp":
                case "image/vnd.wap.wbmp":
                    return ("WAP Bitamp (WBMP);", "image/vnd.wap.wbmp", ".wbmp", "IANA: WBMP", true);
                case "wav":
                case "audio/x-wav":
                    return ("Waveform Audio File Format (WAV);", "audio/x-wav", ".wav", "Wikipedia: WAV", true);
                case "davmount":
                case "application/davmount+xml":
                    return ("Web Distributed Authoring and Versioning", "application/davmount+xml", ".davmount",
                        "RFC 4918", true);
                case "woff":
                case "application/x-font-woff":
                    return ("Web Open Font Format", "application/x-font-woff", ".woff",
                        "Wikipedia: Web Open Font Format", true);
                case "wspolicy":
                case "application/wspolicy+xml":
                    return ("Web Services Policy", "application/wspolicy+xml", ".wspolicy", "W3C Web Services Policy",
                        true);
                case "webp":
                case "image/webp": return ("WebP Image", "image/webp", ".webp", "Wikipedia: WebP", true);
                case "wtb":
                case "application/vnd.webturbo":
                    return ("WebTurbo", "application/vnd.webturbo", ".wtb", "IANA: WebTurbo", true);
                case "wgt":
                case "application/widget":
                    return ("Widget Packaging and XML Configuration", "application/widget", ".wgt",
                        "W3C Widget Packaging and XML Configuration", true);
                case "hlp":
                case "application/winhlp": return ("WinHelp", "application/winhlp", ".hlp", "Wikipedia: WinHelp", true);
                case "wml":
                case "text/vnd.wap.wml":
                    return ("Wireless Markup Language (WML);", "text/vnd.wap.wml", ".wml", "Wikipedia: WML", true);
                case "wmls":
                case "text/vnd.wap.wmlscript":
                    return ("Wireless Markup Language Script (WMLScript);", "text/vnd.wap.wmlscript", ".wmls",
                        "Wikipedia: WMLScript", true);
                case "wmlsc":
                case "application/vnd.wap.wmlscriptc":
                    return ("WMLScript", "application/vnd.wap.wmlscriptc", ".wmlsc", "IANA: WMLScript", true);
                case "wpd":
                case "application/vnd.wordperfect":
                    return ("Wordperfect", "application/vnd.wordperfect", ".wpd", "IANA: Wordperfect", true);
                case "stf":
                case "application/vnd.wt.stf":
                    return ("Worldtalk", "application/vnd.wt.stf", ".stf", "IANA: Worldtalk", true);
                case "wsdl":
                case "application/wsdl+xml":
                    return ("WSDL - Web Services Description Language", "application/wsdl+xml", ".wsdl",
                        "W3C Web Service Description Language", true);
                case "xbm":
                case "image/x-xbitmap": return ("X BitMap", "image/x-xbitmap", ".xbm", "Wikipedia: X BitMap", true);
                case "xpm":
                case "image/x-xpixmap": return ("X PixMap", "image/x-xpixmap", ".xpm", "Wikipedia: X PixMap", true);
                case "xwd":
                case "image/x-xwindowdump":
                    return ("X Window Dump", "image/x-xwindowdump", ".xwd", "Wikipedia: X Window Dump", true);
                case "der":
                case "application/x-x509-ca-cert":
                    return ("X.509 Certificate", "application/x-x509-ca-cert", ".der", "Wikipedia: X.509", true);
                case "fig":
                case "application/x-xfig": return ("Xfig", "application/x-xfig", ".fig", "Wikipedia: Xfig", true);
                case "xhtml":
                case "application/xhtml+xml":
                    return ("XHTML - The Extensible HyperText Markup Language", "application/xhtml+xml", ".xhtml",
                        "W3C XHTML", true);
                case "xml":
                case "application/xml":
                    return ("XML - Extensible Markup Language", "application/xml", ".xml", "W3C XML", true);
                case "xdf":
                case "application/xcap-diff+xml":
                    return ("XML Configuration Access Protocol - XCAP Diff", "application/xcap-diff+xml", ".xdf",
                        "Wikipedia: XCAP", true);
                case "xenc":
                case "application/xenc+xml":
                    return ("XML Encryption Syntax and Processing", "application/xenc+xml", ".xenc",
                        "W3C XML Encryption Syntax and Processing", true);
                case "xer":
                case "application/patch-ops-error+xml":
                    return ("XML Patch Framework", "application/patch-ops-error+xml", ".xer", "RFC 5261", true);
                case "rl":
                case "application/resource-lists+xml":
                    return ("XML Resource Lists", "application/resource-lists+xml", ".rl", "RFC 4826", true);
                case "rs":
                case "application/rls-services+xml":
                    return ("XML Resource Lists", "application/rls-services+xml", ".rs", "RFC 4826", true);
                case "rld":
                case "application/resource-lists-diff+xml":
                    return ("XML Resource Lists Diff", "application/resource-lists-diff+xml", ".rld", "RFC 4826", true);
                case "xslt":
                case "application/xslt+xml":
                    return ("XML Transformations", "application/xslt+xml", ".xslt", "W3C XSLT", true);
                case "xop":
                case "application/xop+xml":
                    return ("XML-Binary Optimized Packaging", "application/xop+xml", ".xop", "W3C XOP", true);
                case "xpi":
                case "application/x-xpinstall":
                    return ("XPInstall - Mozilla", "application/x-xpinstall", ".xpi", "Wikipedia: XPI", true);
                case "xspf":
                case "application/xspf+xml":
                    return ("XSPF - XML Shareable Playlist Format", "application/xspf+xml", ".xspf",
                        "XML Shareable Playlist Format", true);
                case "xul":
                case "application/vnd.mozilla.xul+xml":
                    return ("XUL - XML User Interface Language", "application/vnd.mozilla.xul+xml", ".xul", "IANA: XUL",
                        true);
                case "xyz":
                case "chemical/x-xyz":
                    return ("XYZ File Format", "chemical/x-xyz", ".xyz", "Wikipedia: XYZ File Format", true);
                case "yaml":
                case "text/yaml":
                    return ("YAML Ain't Markup Language / Yet Another Markup Language", "text/yaml", ".yaml",
                        "YAML: YAML Ain't Markup Language", true);
                case "yang":
                case "application/yang":
                    return ("YANG Data Modeling Language", "application/yang", ".yang", "Wikipedia: YANG", true);
                case "yin":
                case "application/yin+xml":
                    return ("YIN (YANG - XML);", "application/yin+xml", ".yin", "Wikipedia: YANG", true);
                case "zir":
                case "application/vnd.zul":
                    return ("Z.U.L. Geometry", "application/vnd.zul", ".zir", "IANA: Z.U.L.", true);
                case "zip":
                case "application/zip": return ("Zip Archive", "application/zip", ".zip", "Wikipedia: Zip", true);
                case "zmm":
                case "application/vnd.handheld-entertainment+xml":
                    return ("ZVUE Media Manager", "application/vnd.handheld-entertainment+xml", ".zmm",
                        "IANA: ZVUE Media Manager", true);
                case "zaz":
                case "application/vnd.zzazz.deck+xml":
                    return ("Zzazz Deck", "application/vnd.zzazz.deck+xml", ".zaz", "IANA: Zzazz", false);
                case "pps":
                    return ("PPS", "application/pps", ".pps", null, true);
                case "gpx":
                    return ("GPX", "text/gpx", ".gpx", null, true);
                case "gml":
                    return ("GML", "text/gml", ".gml", null, true);
            }
        }
    }
}
