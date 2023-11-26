const axios = require('axios');

// PositionStack API access key
const apiKey = '5abcb0b627b6519cbbe47c0d7feb8809';

// Coordinates (latitude and longitude)
const latitude = -23.62427221; // Replace with your latitude
const longitude = -46.70136611; // Replace with your longitude

// Construct the URL for the PositionStack API request
const apiUrl = `http://api.positionstack.com/v1/reverse?access_key=${apiKey}&query=${latitude},${longitude}`;

// Make the API request
axios
  .get(apiUrl)
  .then((response) => {
    // Extract the address information from the API response
    const data = response.data.data[1];
    if (data) {
      const address = data; // The addresss
      console.log(address);
    } else {
      console.error('No address found for the given coordinates.');
    }
  })
  .catch((error) => {
    console.error('Error:', error.message);
  });

