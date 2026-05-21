const Brand = require('../models/Brand');

// Data Access Layer for the auxiliary Brand entity
class BrandRepository {
  findAll() {
    return Brand.findAll({ order: [['name', 'ASC']] });
  }

  findById(id) {
    return Brand.findByPk(id);
  }
}

module.exports = new BrandRepository();
