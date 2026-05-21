const carRepository = require('../repositories/CarRepository');

// Business Logic Layer for the main Car entity
class CarService {
  getAllCars() {
    return carRepository.findAll();
  }

  getCarById(id) {
    return carRepository.findById(id);
  }

  createCar({ model, year, price, color, brandId }) {
    if (!model || !year || !price || !color || !brandId) {
      throw new Error('All fields are required');
    }
    return carRepository.create({ model, year: +year, price: +price, color, brandId: +brandId });
  }

  updateCar(id, { model, year, price, color, brandId }) {
    if (!model || !year || !price || !color || !brandId) {
      throw new Error('All fields are required');
    }
    return carRepository.update(id, { model, year: +year, price: +price, color, brandId: +brandId });
  }

  deleteCar(id) {
    return carRepository.delete(id);
  }
}

module.exports = new CarService();
