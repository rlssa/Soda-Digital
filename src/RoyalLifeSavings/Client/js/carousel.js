import Embla from "embla-carousel";
import { setupDotBtns, generateDotBtns, selectDotBtn } from "./carousel-dots";


const carouselOptions = {
    draggable: true,
    align: "start",
    containScroll: "keepSnaps",
    slidesToScroll: 1,
};

const notDraggable = {
    draggable: false,
    align: "start",
};


const emblaCards = document.querySelector(".books");
const booksCard = document.getElementsByClassName("books-card");
const dots = document.getElementById("embla__dots");
const books = document.getElementById("books");
const mediaQueryXl = window.matchMedia('(min-width: 1423px)')
const mediaQueryLg = window.matchMedia('(min-width: 1199px)')
const mediaQueryMd = window.matchMedia('(min-width: 767px)')
const mediaQuerySm = window.matchMedia('(min-width: 600px)')

if(emblaCards){
    if(books.length <= 6 && mediaQueryXl.matches) {
        const dotsNode = emblaCards.querySelector(".embla__dots");
        const embla = Embla(emblaCards, notDraggable);
        const dotNodesArray = generateDotBtns(dotsNode, embla);
        setupDotBtns(dotNodesArray, embla);
    
        const selectDotButtons = selectDotBtn(dotNodesArray, embla);
        embla.on("select", selectDotButtons);
        embla.on("init", selectDotButtons);
    } else if(books.length <= 5 && mediaQueryLg.matches) {
        const dotsNode = emblaCards.querySelector(".embla__dots");
        const embla = Embla(emblaCards, notDraggable);
        const dotNodesArray = generateDotBtns(dotsNode, embla);
        setupDotBtns(dotNodesArray, embla);
    
        const selectDotButtons = selectDotBtn(dotNodesArray, embla);
        embla.on("select", selectDotButtons);
        embla.on("init", selectDotButtons);
    }else if(mediaQueryMd.matches && booksCard.length <= 4) {
        const dotsNode = emblaCards.querySelector(".embla__dots");
        const embla = Embla(emblaCards, notDraggable);
        const dotNodesArray = generateDotBtns(dotsNode, embla);
        setupDotBtns(dotNodesArray, embla);
    
        const selectDotButtons = selectDotBtn(dotNodesArray, embla);
        embla.on("select", selectDotButtons);
        embla.on("init", selectDotButtons);
    }else if(mediaQuerySm.matches && booksCard.length <= 1) {
        const dotsNode = emblaCards.querySelector(".embla__dots");
        const embla = Embla(emblaCards, notDraggable);
        const dotNodesArray = generateDotBtns(dotsNode, embla);
        setupDotBtns(dotNodesArray, embla);
    
        const selectDotButtons = selectDotBtn(dotNodesArray, embla);
        embla.on("select", selectDotButtons);
        embla.on("init", selectDotButtons);
    }else {
        const dotsNode = emblaCards.querySelector(".embla__dots");
        const embla = Embla(emblaCards, carouselOptions);
        const dotNodesArray = generateDotBtns(dotsNode, embla);
        setupDotBtns(dotNodesArray, embla);
    
        const selectDotButtons = selectDotBtn(dotNodesArray, embla);
        embla.on("select", selectDotButtons);
        embla.on("init", selectDotButtons);
    }
}

if(booksCard.length <= 6 && mediaQueryXl.matches) {
    dots.classList.add("d-none");
  } else if(booksCard.length <= 5 && mediaQueryLg.matches) {
    dots.classList.add("d-none");
  }else if(mediaQueryMd.matches && booksCard.length <= 4) {
    dots.classList.add("d-none");
  } else if(mediaQuerySm.matches && booksCard.length <= 1) {
    dots.classList.add("d-none");
  } else {
    books.classList.add("grabbable")
  }







