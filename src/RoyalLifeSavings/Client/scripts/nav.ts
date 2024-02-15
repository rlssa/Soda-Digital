const dropdown = document.getElementById("dropdownMenuButton1");

if (dropdown) {
  dropdown.addEventListener("click", showItem);
}

function showItem() {
  const showCollapse = document.getElementById("collapse");
  showCollapse.classList.toggle("show");
}

window.onclick = function (event) {
  if (!event.target.matches(".dropdown-toggle")) {
    var dropdowns = document.getElementsByClassName("dropdown-menu");
    var i;
    for (i = 0; i < dropdowns.length; i++) {
      var openDropdown = dropdowns[i];
      if (openDropdown.classList.contains("show")) {
        openDropdown.classList.remove("show");
      }
    }
  }
};

document.addEventListener("keydown", function (event) {
  if (event.key === "Escape") {
    var dropdowns = document.getElementsByClassName("dropdown-menu");
    var i;
    for (i = 0; i < dropdowns.length; i++) {
      var openDropdown = dropdowns[i];
      if (openDropdown.classList.contains("show")) {
        openDropdown.classList.remove("show");
      }
    }
  }
});

const toggle = document.querySelector(".masthead-toggle > a");
const close = document.querySelector(".masthead-button > div > a");

const body = document.querySelector("body > main");

if (toggle) {
  toggle.addEventListener("click", (e) => {
    e.preventDefault();
    toggle.closest(".masthead").classList.toggle("masthead-button--open");

    body.classList.add("opacity");

    document.body.classList.add("hidden");
  });

  document.body.classList.toggle("masthead-button--open");
}

if (close) {
  close.addEventListener("click", (e) => {
    e.preventDefault();
    close.closest(".masthead").classList.toggle("masthead-button--open");

    body.classList.remove("opacity");

    document.body.classList.remove("hidden");
  });
}

const activePage = location.href;
document.querySelectorAll("nav .links").forEach((link) => {
  if (link instanceof HTMLAnchorElement && link.href === `${activePage}`) {
    link.classList.add("active");
  }
});


export default undefined;
