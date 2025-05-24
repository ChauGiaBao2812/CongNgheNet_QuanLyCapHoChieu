function toggleRequirements() {
  const box = document.getElementById("requirementBox");
  const error = document.getElementById("error-icon");
  box.style.display = box.style.display === "block" ? "none" : "block";
  error.style.display = box.style.display === "block" ? "none" : "block";
}
