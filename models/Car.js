const { DataTypes } = require('sequelize');
const sequelize = require('../db/database');
const Brand = require('./Brand');

// Main entity – full CRUD operations are performed on Car
const Car = sequelize.define('Car', {
  id: {
    type: DataTypes.INTEGER,
    primaryKey: true,
    autoIncrement: true,
  },
  model: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  year: {
    type: DataTypes.INTEGER,
    allowNull: false,
  },
  price: {
    type: DataTypes.FLOAT,
    allowNull: false,
  },
  color: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  brandId: {
    type: DataTypes.INTEGER,
    allowNull: false,
    references: { model: Brand, key: 'id' },
  },
}, {
  tableName: 'cars',
  timestamps: false,
});

// One-to-many: one Brand → many Cars
Brand.hasMany(Car, { foreignKey: 'brandId', onDelete: 'RESTRICT' });
Car.belongsTo(Brand, { foreignKey: 'brandId' });

module.exports = Car;
