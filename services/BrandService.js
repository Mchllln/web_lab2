const brandRepository = require('../repositories/BrandRepository');

// Business Logic Layer for the auxiliary Brand entity
class BrandService {
  getAllBrands() {
    return brandRepository.findAll();
  }

  getBrandById(id) {
    return brandRepository.findById(id);
  }
}

module.exports = new BrandService();
