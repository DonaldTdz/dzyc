
$.fn.iWebOffice = function () {
    var str = '<object id="WebOffice" width="100%" height="100%" classid="clsid:23739A7E-2000-4D1C-88D5-D50B18F7C347" codebase="../../Handler/iWebOffice2000.ocx">';
    str += '</object>';
    this.append(str);
    this.css("width", $(window).width());
    this.css("height", $(window).height());
    WebOffice.WebUrl = arguments[0].WebUrl;
    WebOffice.RecordID = arguments[0].RecordID;               //�ĵ���¼��
    WebOffice.Template = "";                    //ģ���¼��
    WebOffice.FileName = arguments[0].FileName;          //�ĵ�����
    WebOffice.FileType = arguments[0].FileType;              //�ĵ�����  .doc  .xls
    WebOffice.UserName = "�ĵ��༭��";          //��ǰ����Ա
    WebOffice.EditType = "1";                   //�༭״̬����һλ����Ϊ0,1,2,3���У�0���ɱ༭��1���Ա༭,�޺ۼ���2���Ա༭,�кۼ�,�����޶���3���Ա༭,�кۼ�,���޶���

    WebOffice.MaxFileSize = 4 * 1024;         //�����ĵ���С���ƣ�Ĭ����8M���������ó�4M��
    WebOffice.ShowMenu = "1";                   //�Ƿ���ʾ�˵���1��ʾ��0����ʾ

    // WebOffice.CreateFile();                   //�����հ��ĵ�

    //�򿪷�������docment
    WebOffice.loadDocument = function () {
        if (!WebOffice.WebOpen()) {                //�򿪸��ĵ�    ����OfficeServer��OPTION="LOADFILE"
            StatusMsg(WebOffice.Status);            //��ʾ״̬����OfficeServer�ж�ȡ
        } else {
            StatusMsg(WebOffice.Status);            //��ʾ״̬����OfficeServer�ж�ȡ
        }
    };

    //�����ļ�
    WebOffice.createFile = function () {
        WebOffice.CreateFile();
    };
    //�����ļ���������
    WebOffice.saveDocument = function () {
        if (!WebOffice.WebSave()) {     //����OfficeServer��OPTION="SAVEFILE"
            StatusMsg(WebOffice.Status);
        } else {
            StatusMsg(WebOffice.Status);
        }
    };
    return WebOffice;

}
//���ã���ʾ����״̬
function StatusMsg(mString) {
    //StatusBar.innerHTML = mString;
}

//��ʼ������
function initObject() {
    WebOffice.WebUrl = "OfficeServer.aspx";      //��̨����ҳ·��������ִ�к�̨���ݴ���ҵ�񡣱�����֧�����·��
    WebOffice.RecordID = "1234";                //�ĵ���¼��
    WebOffice.Template = "";                    //ģ���¼��
    WebOffice.FileName = "1234.doc";            //�ĵ�����
    WebOffice.FileType = ".doc";                //�ĵ�����  .doc  .xls
    WebOffice.UserName = "�ĵ��༭��";          //��ǰ����Ա
    WebOffice.EditType = "1";                   //�༭״̬����һλ����Ϊ0,1,2,3���У�0���ɱ༭��1���Ա༭,�޺ۼ���2���Ա༭,�кۼ�,�����޶���3���Ա༭,�кۼ�,���޶���

    WebOffice.MaxFileSize = 4 * 1024;         //�����ĵ���С���ƣ�Ĭ����8M���������ó�4M��
    WebOffice.ShowMenu = "1";                   //�Ƿ���ʾ�˵���1��ʾ��0����ʾ

    WebOffice.CreateFile();                   //�����հ��ĵ�
}

//���ã��򿪷������ĵ�
function LoadDocument() {
    if (!WebOffice.WebOpen()) {                //�򿪸��ĵ�    ����OfficeServer��OPTION="LOADFILE"
        StatusMsg(WebOffice.Status);            //��ʾ״̬����OfficeServer�ж�ȡ
    } else {
        StatusMsg(WebOffice.Status);            //��ʾ״̬����OfficeServer�ж�ȡ
    }
}

//���ã�����������ĵ�
function SaveDocument() {
    if (!WebOffice.WebSave()) {     //����OfficeServer��OPTION="SAVEFILE"
        StatusMsg(WebOffice.Status);
    } else {
        StatusMsg(WebOffice.Status);
    }
}

//���ã������հ��ĵ�
function CreateFile() {
    WebOffice.CreateFile();
    StatusMsg(WebOffice.Status);
}

//���ã��򿪱����ļ�
function WebOpenLocal() {
    var result = WebOffice.WebOpenLocalFile();
    if (result) {
        StatusMsg("�򿪱����ĵ��ɹ���");
    } else {
        StatusMsg(WebOffice.Status);
    }
}

//���ã������ļ�������
function WebSaveLocal() {
    var result = WebOffice.WebSaveLocalFile();
    if (result) {
        StatusMsg("�����ĵ������سɹ���");
    } else {
        StatusMsg(WebOffice.Status);
    }
}

//���ã���ȡ�ĵ�ҳ����VBA��չӦ�ã�
function WebDocumentPageCount() {
    if (WebOffice.FileType == ".doc") {
        var intPageTotal = WebOffice.WebObject.Application.ActiveDocument.BuiltInDocumentProperties(14);
        alert("�ĵ�ҳ������" + intPageTotal);
    }

    if (WebOffice.FileType == ".wps") {
        var intPageTotal = WebOffice.WebObject.PagesCount();
        alert("�ĵ�ҳ������" + intPageTotal);
    }
}

//���ã������ĵ���ȫ���ۼ���VBA��չӦ�ã�
function WebAcceptAllRevisions() {
    WebOffice.WebObject.Application.ActiveDocument.AcceptAllRevisions();
    var mCount = WebOffice.WebObject.Application.ActiveDocument.Revisions.Count;
    if (mCount > 0) {
        StatusMsg("���ܺۼ�ʧ�ܣ�");
        return false;
    } else {
        StatusMsg("�ĵ��еĺۼ��Ѿ�ȫ�����ܣ�");
        return true;
    }
}

//���ã��˳�iWebOffice
function UnLoad() {
    try {
        if (!WebOffice.WebClose()) {
            StatusMsg(WebOffice.Status);
        }
        else {
            StatusMsg("�ر��ĵ�...");
        }
    }
    catch (e) {
        alert(e.description);
    }
}