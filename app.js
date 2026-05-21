const express = require('express');
const methodOverride = require('method-override');
const path = require('path');
const sequelize = require('./db/database');

// Import models to register associations before sync
require('./models/Brand');
require('./models/Car');

const carRoutes = require('./routes/carRoutes');

const app = express();
const PORT = process.env.PORT || 3000;

// Template engine
app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

// Middleware
app.use(express.urlencoded({ extended: true }));
app.use(express.json());

// method-override reads _method from query string so HTML forms can send PUT/DELETE
app.use(methodOverride('_method'));

// Static files (if any CSS/JS assets are added later)
app.use(express.static(path.join(__dirname, 'public')));

// Routes
app.use('/cars', carRoutes);

// Redirect root to /cars
app.get('/', (req, res) => res.redirect('/cars'));

// Sync DB schema, then start server
sequelize.sync().then(() => {
  app.listen(PORT, () => {
    console.log(`Server running at http://localhost:${PORT}`);
    console.log('Run "npm run seed" first if the database is empty.');
  });
}).catch(err => {
  console.error('Failed to connect to database:', err);
  process.exit(1);
});
