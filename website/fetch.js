fetch('https://PORT NUMBER HERE}/api/resource')
    .then(response => response.json())
    .then(data => console.log('Received:', data))
    .catch(error => console.error('Error:', error));
