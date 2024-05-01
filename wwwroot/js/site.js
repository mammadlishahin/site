// var header = document.querySelector("header");
// var nav = document.querySelector("nav");

var prevScrollpos = window.pageYOffset
function NavBarTenzimleme(scrol_top) {
  var currentScrollPos = window.pageYOffset;
  var nav_style = window.getComputedStyle(document.querySelector("nav"));

  var navYukseklik = nav_style.getPropertyValue("height");


  // Yuxarı gedərkən
  if (prevScrollpos > currentScrollPos) {
    if (scrol_top == 0) {
      document.querySelector("header").style.paddingTop = navYukseklik;
    }
    document.querySelector("nav").style.top = "0";
    document.querySelector("nav").classList.add("navbar-scrolled");
    document.querySelector("nav").style.boxShadow = "rgb(99 131 116 / 77%) 0px 5px 10px 0px";

    // Aşağı gedərkən
  } else if (window.pageYOffset > 1) {
    document.querySelector("nav").style.top = "-" + navYukseklik;
    document.querySelector("header").style.paddingTop = "0px";
    document.querySelector("nav").style.boxShadow = "none";
    document.querySelector("nav").classList.remove("navbar-scrolled");
  }
  prevScrollpos = currentScrollPos;
}


window.addEventListener('load', function () {
  document.querySelector("header").style.paddingTop = window.getComputedStyle(document.querySelector("nav")).getPropertyValue("height");




  window.onscroll = function () {
    // console.log(prevScrollpos)
    var d = document.documentElement;
    NavBarTenzimleme(d.scrollTop);
  }

});






// ////////////////////////////////////////////
try {
  var aes = document.querySelectorAll(".social i");
  var socialContainer = document.querySelector(".social");
  aes.forEach(element => {
    socialContainer.style.width = element.offsetWidth * 2 + "px";
  });
} catch (error) {

}




try {
  // Ekran genişliğini dinlemek için bir fonksiyon oluşturun
  function handleResize() {
    var cikisYapmaDropdownUl = document.querySelector("form.dropdown ul");
    // Ekran genişliğini alın
    var screenWidth = window.innerWidth;
    if (cikisYapmaDropdownUl != null) {
      if (screenWidth < 991) {
        document.querySelector("form.dropdown ul").style.left = "0%";

      } else {
        document.querySelector("form.dropdown ul").style.left = "-100%";
      }
    }

  }

  // Sayfa yüklendiğinde ve ekran boyutu değiştiğinde handleResize fonksiyonunu çağırın
  window.addEventListener("load", handleResize);
  window.addEventListener("resize", handleResize);
} catch (error) {
  console.error(error)
  console.error("site.js dosyasinda problem var")
}
//////////////////////////////////////////////////////
function getTransformX(element) {
  var computedStyle = window.getComputedStyle(element);
  var transformValue = computedStyle.getPropertyValue("transform");
  if (transformValue && transformValue !== "none") {
    // transformValue bir matris olabilir, bu nedenle x değerini çıkarmak için parçalıyoruz
    var matrix = transformValue.match(/^matrix\((.+)\)$/);
    if (matrix) {
      var matrixValues = matrix[1].split(",");
      if (matrixValues.length >= 6) {
        var xValue = parseFloat(matrixValues[4].trim());
        return xValue;
      }
    }
  }
  return 0; // Transform-x değeri bulunamadı veya değer "none" olarak döndü
}

// Örnek kullanım





// function ElementSagdanNeKadarIceride(element) {
//   var rect = element.getBoundingClientRect();
//   var screenWidth = window.innerWidth;
//   var distanceFromRight = screenWidth - rect.right;
//   return distanceFromRight;
// }
// function ElementSoldanNeKadarIceride(element) {
//   var rect = element.getBoundingClientRect();
//   var distanceFromLeft = rect.left;
//   return distanceFromLeft;
// }

// // Örnek kullanım






// function SagVeSolBosluk(element) {
//   //var rect = element.getBoundingClientRect();
//   var ulninGenisligi = element.offsetWidth;
//   var EkranGenisligi = window.innerWidth;
//   var kenarlBosluk = EkranGenisligi - ulninGenisligi;
//   kenarlBosluk = kenarlBosluk / 2;
//   return kenarlBosluk;
// }

// // Örnek kullanım
// window.addEventListener("resize", function () {
//   var element = document.querySelector(".one-cikan-mekaleler ul.show");
//   // var kenarlBosluk = SagVeSolBosluk(element);
//   var artan = 1;
//   do {
    
//     var SagdanNeKadarIceride = ElementSagdanNeKadarIceride(element);
//     var SoldanNeKadarIceride = ElementSoldanNeKadarIceride(element);
//     var ElementinTransformXdeyeri = getTransformX(element);
//     var aradakiFerq = Math.abs(SagdanNeKadarIceride - SoldanNeKadarIceride);

//     if (ElementinTransformXdeyeri > 0) {
//       var currentTransform = window.getComputedStyle(element).transform;
//       element.style.transform = currentTransform + ' translateX(' + artan + 'px)';
//     } else {
//       var currentTransform = window.getComputedStyle(element).transform;
//       console.log(' translateX(' + artan + 'px)')
//       element.style.transform = ' translateX(' + artan + 'px)';
//     }
//     artan++
//   } while (artan<50)  //(aradakiFerq > 5);


//   // console.log("kenarlBosluk", kenarlBosluk);

//   // console.log("Element ekranın sağında", SagdanNeKadarIceride, "piksel içeride");
//   // console.log("Element ekranın Soldan", SoldanNeKadarIceride, "piksel içeride");


//   // 
//   // console.log("Elementin transform-x değeri:", transformXValue);
// });

