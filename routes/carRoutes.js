const express = require('express');
const router = express.Router();
const CarController = require('../controllers/CarController');

// List all cars
router.get('/', CarController.index);

// Show form to create a new car
router.get('/new', CarController.newForm);

// Show form to view/edit an existing car
router.get('/:id', CarController.editForm);

// Create a new car
router.post('/', CarController.create);

// Update an existing car (method-override turns POST _method=PUT into PUT)
router.put('/:id', CarController.update);

// Delete a car (method-override turns POST _method=DELETE into DELETE)
router.delete('/:id', CarController.destroy);

module.exports = router;
