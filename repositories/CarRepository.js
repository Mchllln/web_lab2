const Car = require('../models/Car');
const Brand = require('../models/Brand');

// Data Access Layer for the main Car entity
class CarRepository {
  findAll() {
    // Include Brand so the list page can show the brand id (and name for display)
    return Car.findAll({
      include: [{ model: Brand }],
      order: [['id', 'ASC']],
    });
  }

  findById(id) {
    return Car.findByPk(id, { include: [{ model: Brand }] });
  }

  create(data) {
    return Car.create(data);
  }

  async update(id, data) {
    const car = await Car.findByPk(id);
    if (!car) throw new Error(`Car with id=${id} not found`);
    return car.update(data);
  }

  async delete(id) {
    const car = await Car.findByPk(id);
    if (!car) throw new Error(`Car with id=${id} not found`);
    return car.destroy();
  }
}

module.exports = new CarRepository();
