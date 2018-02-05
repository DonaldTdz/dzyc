
var stores_obj = {
    stores_init: function (args) {
        global_obj.map_init();
        global_obj.file_upload($('#ImgUpload'), $('form input[name=ImgPath]'), $('#ImgDetail'), args.uploadAction);
        $('#ImgDetail').html(global_obj.img_link($('form input[name=ImgPath]').val()));

        //shaosc add
        $('#stores_form input[name="submit_button"]').click(function () {
            if (global_obj.check_form($('*[notnull]'))) {
                return false;
            }
            $(this).attr('disabled', true);

            var Id = $("input[name='Id']").attr("value"), LBSShopHeaderId = $("input[name='LBSShopHeaderId']").attr("value"), Name = $("input[name='StoresName']").attr("value"),
                            ImgPath = $("input[name='ImgPath']").attr("value"), Address = $("#Address").attr("value"), Telephone = $("#Telephone").attr("value"),
                            Longitude = $("input[name='PrimaryLng']").attr("value"), Latitude = $("input[name='PrimaryLat']").attr("value");
            var params = { Id: Id, LBSShopHeaderId: LBSShopHeaderId, Name: Name, ImgPath: ImgPath, ShopAddress: Address, PhoneNumber: Telephone, Longitude: Longitude, Latitude: Latitude };

            $(this).attr('disabled', true);
            $.post(args.submitAction, params, function (data) {
                if (data.status == 1) {
                    window.location="ShopInfo";
                } else {
                    alert(data.msg);
                    $('#config_form input:submit').attr('disabled', false);
                }
            }, 'json');
        });
        //shaosc add end
    }
}
