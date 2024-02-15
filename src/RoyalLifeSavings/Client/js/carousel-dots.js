export const setupDotBtns = (dotNodesArray, embla) => {
    dotNodesArray.forEach((dotNode, i) => {
        dotNode.classList.add("embla__dot");
        dotNode.addEventListener("click", () => embla.scrollTo(i), false);
    });
};

export const generateDotBtns = (dotsNode, embla) => {
    const scrollSnaps = embla.scrollSnapList();
    const dotsFrag = document.createDocumentFragment();
    const dotNodesArray = scrollSnaps.map(() => document.createElement("button"));
    dotNodesArray.forEach(dotNode => dotsFrag.appendChild(dotNode));
    dotsNode.appendChild(dotsFrag);
    return dotNodesArray;
};

export const selectDotBtn = (dotNodesArray, embla) => () => {
    const selected = embla.selectedScrollSnap();
    const previous = embla.previousScrollSnap();
    dotNodesArray[previous].classList.remove("is-selected");
    dotNodesArray[selected].classList.add("is-selected");
};
