/**
 * Seed script – populates auxiliary entity (Brand) with initial records.
 * Lab requirement: auxiliary records must be pre-seeded; users cannot create
 * them through the web interface.
 * Run once with: node db/seed.js
 */
const sequelize = require('./database');
const Brand = require('../models/Brand');
const Car = require('../models/Car');

async function seed() {
  await sequelize.sync({ force: true });

  const brands = await Brand.bulkCreate([
    { name: 'Toyota',      country: 'Japan',   foundedYear: 1937 },
    { name: 'BMW',         country: 'Germany', foundedYear: 1916 },
    { name: 'Ford',        country: 'USA',     foundedYear: 1903 },
    { name: 'Honda',       country: 'Japan',   foundedYear: 1948 },
    { name: 'Mercedes',    country: 'Germany', foundedYear: 1926 },
  ]);

  await Car.bulkCreate([
    { model: 'Camry',      year: 2021, price: 26000, color: 'White',  brandId: brands[0].id },
    { model: 'Corolla',    year: 2020, price: 21000, color: 'Silver', brandId: brands[0].id },
    { model: '3 Series',   year: 2022, price: 43000, color: 'Black',  brandId: brands[1].id },
    { model: 'Mustang',    year: 2023, price: 38000, color: 'Red',    brandId: brands[2].id },
    { model: 'Civic',      year: 2022, price: 24000, color: 'Blue',   brandId: brands[3].id },
    { model: 'C-Class',    year: 2023, price: 47000, color: 'White',  brandId: brands[4].id },
  ]);

  console.log('Database seeded successfully.');
  await sequelize.close();
}

seed().catch((err) => {
  console.error('Seed failed:', err);
  process.exit(1);
});
