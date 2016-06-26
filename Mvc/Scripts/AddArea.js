document.addEventListener('DOMContentLoaded', function() {
  var addButton = document.getElementById('add-tag-button');
  var tagNameInput = document.getElementById('tag-name');
  var tagsContainer = document.getElementById('tags-container');

  var createTag = function(tagText) {
    newElement = document.createElement('div');
    newElement.innerHTML = `${tagText} <button class="delete-tag-button">X</button>`;
    newElement.classList.add('element');
    return newElement;
  };

  tagsContainer.addEventListener('click', function(event) {
    if(event.target.classList.contains('delete-tag-button')) {
      tagsContainer.removeChild(event.target.parentNode);
    }
  });

  addButton.addEventListener('click', function() {
    if(tagNameInput.value) {
      var newElement = createTag(tagNameInput.value);
      tagNameInput.value = '';
      tagsContainer.appendChild(newElement);
    }
  });
});
