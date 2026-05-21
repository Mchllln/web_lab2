const { DataTypes } = require('sequelize');
const sequelize = require('../db/database');

// Auxiliary entity – one Brand has many Cars (1:N relationship)
const Brand = sequelize.define('Brand', {
  id: {
    type: DataTypes.INTEGER,
    primaryKey: true,
    autoIncrement: true,
  },
  name: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  country: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  foundedYear: {
    type: DataTypes.INTEGER,
    allowNull: false,
  },
}, {
  tableName: 'brands',
  timestamps: false,
});

module.exports = Brand;
