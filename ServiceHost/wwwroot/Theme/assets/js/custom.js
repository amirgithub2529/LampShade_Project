

function addToCart(id , name , price , picture){

    let products = $.cookie("cart-items");
    if (products === undefined) {  //فکر کنم 3 تا مساوی یعنی تایپ چک هم بکنه
        products = [];
    } else {
        products = JSON.parse(products); //در کوکی فقط میشود استرینگ ذخیره کرد
    }

    const count = $("#productCount").val();

    const currentProduct = products.find(x => x.id === id);

    if (currentProduct !== undefined) {
        products.find(x => x.id === id).count = currentProduct.count + parseInt(count);
    }


}