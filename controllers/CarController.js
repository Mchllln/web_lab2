const carService = require('../services/CarService');
const brandService = require('../services/BrandService');

/**
 * Controller layer – bridges HTTP requests with services and renders EJS views.
 * All CRUD actions for the main Car entity are handled here.
 */
const CarController = {

  // GET /cars – List page: shows all cars; brand info shown partially (id only in table)
  async index(req, res) {
    try {
      const cars = await carService.getAllCars();
      res.render('cars/list', { cars, title: 'Car Dealership – All Cars' });
    } catch (err) {
      res.status(500).send(err.message);
    }
  },

  // GET /cars/new – Form page (create mode)
  async newForm(req, res) {
    try {
      const brands = await brandService.getAllBrands();
      res.render('cars/form', { car: null, brands, title: 'Add New Car', errors: [] });
    } catch (err) {
      res.status(500).send(err.message);
    }
  },

  // GET /cars/:id – Form page (view/edit mode): brand shown fully via dropdown
  async editForm(req, res) {
    try {
      const car = await carService.getCarById(req.params.id);
      if (!car) return res.status(404).send('Car not found');
      const brands = await brandService.getAllBrands();
      res.render('cars/form', { car, brands, title: `Edit Car – ${car.model}`, errors: [] });
    } catch (err) {
      res.status(500).send(err.message);
    }
  },

  // POST /cars – Create a new car record
  async create(req, res) {
    try {
      await carService.createCar(req.body);
      res.redirect('/cars');
    } catch (err) {
      const brands = await brandService.getAllBrands();
      res.render('cars/form', { car: null, brands, title: 'Add New Car', errors: [err.message] });
    }
  },

  // PUT /cars/:id – Update an existing car record
  async update(req, res) {
    try {
      await carService.updateCar(req.params.id, req.body);
      res.redirect('/cars');
    } catch (err) {
      const brands = await brandService.getAllBrands();
      const car = await carService.getCarById(req.params.id);
      res.render('cars/form', { car, brands, title: 'Edit Car', errors: [err.message] });
    }
  },

  // DELETE /cars/:id – Delete a car record
  async destroy(req, res) {
    try {
      await carService.deleteCar(req.params.id);
      res.redirect('/cars');
    } catch (err) {
      res.status(500).send(err.message);
    }
  },
};

module.exports = CarController;
