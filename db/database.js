const { Sequelize } = require('sequelize');
const path = require('path');

// SQLite file-based database stored in the db/ folder
const sequelize = new Sequelize({
  dialect: 'sqlite',
  storage: path.join(__dirname, 'dealership.sqlite'),
  logging: false,
});

module.exports = sequelize;
