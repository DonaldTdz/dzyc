//删除元素
function HintRemove(obj) {
    $("#" + obj).remove();
}

//添加进购物车
function CartAdd(obj, webpath, linktype, linkurl, productListId, quantityId) {
    if ($("#" + quantityId).val() == "") {
        return false;
    }
    try {
        if (parseInt($("#" + quantityId).val()) < 1) {
            alert('请输入购买数量');
        }
    }
    catch (e) {
        alert('请输入购买数量');
    }

    $.post('../../Shop/AddToCart', { productListId: productListId, quantity: $("#" + quantityId).val() },
     function (data) {

         if (linktype == 1) {
             location.href = linkurl;
             return;
         }
         $("#cart_info_hint").remove();
         var HintHtml = '<div id="cart_info_hint" class="msg_tips cart_info">'
						+ '<div class="ico"></div>'
						+ '<div class="msg">'
						+ '<strong>商品已成功添加到购物车！</strong>'
						+ '<p>购物车共有<b>' + data + '</b>件商品！'
						+ '<a class="btn btn-success" title="购物篮下手工订单" href="' + linkurl + '">购物篮下手工订单</a>&nbsp;&nbsp;'
						+ '<a title="继续增加商品" href="javascript:;" onclick="HintRemove(\'cart_info_hint\');">继续增加商品</a>'
						+ '<a class="close" title="关闭" href="javascript:;" onclick="HintRemove(\'cart_info_hint\');"><span>关闭</span></a>'
						+ '</div>'
						+ '</div>'
         $(obj).after(HintHtml); //添加节点 
     });
}

//删除购物车商品
function DeleteCart(obj, webpath, goods_id) {
    if (!confirm("您确认要从购物篮中移除吗？") || goods_id == "") {
        return false;
    }
    $.post('../../Shop/delCart', null, function (data) { location.reload(); });
    return false;
}

//计算购物车金额
function CartAmountTotal(obj, webpath, goods_id) {
    if (isNaN($(obj).val())) {
        alert('商品数量只能输入数字!');
        $(obj).val("1");
    }
    $.post('../../Shop/UpdateCart', { productListId: goods_id, quantity: $(obj).val() },
    function (data) {
        location.reload();
    });


}
//购物车数量加减
function CartComputNum(obj, webpath, goods_id, num) {
    if (num > 0) {
        var goods_quantity = $(obj).prev("input[name='goods_quantity']");
        $(goods_quantity).val(parseInt($(goods_quantity).val()) + 1);
        //计算购物车金额
        CartAmountTotal($(goods_quantity), webpath, goods_id);
    } else {
        var goods_quantity = $(obj).next("input[name='goods_quantity']");
        if (parseInt($(goods_quantity).val()) > 1) {
            $(goods_quantity).val(parseInt($(goods_quantity).val()) - 1);
            //计算购物车金额
            CartAmountTotal($(goods_quantity), webpath, goods_id);
        }
    }
}
//计算支付手续费总金额
function PaymentAmountTotal(obj) {
    var payment_price = $(obj).next("input[name='payment_price']").val();
    $("#payment_fee").text(payment_price); //运费
    OrderAmountTotal();
}
//计算配送费用总金额
function FreightAmountTotal(obj) {
    var distribution_price = $(obj).next("input[name='distribution_price']").val();
    $("#freight_amount").text(distribution_price); //运费
    OrderAmountTotal();
}
//计算订单总金额
function OrderAmountTotal() {
    var goods_amount = $("#goods_amount").text(); //商品总金额
    var payment_fee = $("#payment_fee").text(); //手续费
    var freight_amount = $("#freight_amount").text(); //运费
    var order_amount = parseFloat(goods_amount) + parseFloat(payment_fee) + parseFloat(freight_amount); //订单总金额 = 商品金额 + 手续费 + 运费
    $("#order_amount").text(order_amount.toFixed(2));
}