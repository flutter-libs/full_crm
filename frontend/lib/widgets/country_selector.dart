import 'package:flutter/material.dart';

class CountrySelector extends StatefulWidget {
  const CountrySelector({super.key});

  @override
  State<CountrySelector> createState() => _CountrySelectorState();
}

class _CountrySelectorState extends State<CountrySelector> {
  final List<String> _countries = [
    'United States', 'Canada', 'Mexico', 'United Kingdom', 'Germany', 'France', 'Italy',
    'Spain', 'Australia', 'New Zealand', 'India', 'China', 'Japan', 'South Korea',
    'Brazil', 'Argentina', 'South Africa', 'Nigeria', 'Egypt', 'Russia', 'Netherlands',
    'Sweden', 'Norway', 'Denmark', 'Finland', 'Switzerland', 'Austria', 'Poland',
    'Turkey', 'Saudi Arabia', 'United Arab Emirates', 'Thailand', 'Vietnam', 'Philippines',
    'Indonesia', 'Malaysia', 'Pakistan', 'Bangladesh', 'Israel', 'Ireland', 'Belgium',
    'Portugal', 'Czech Republic', 'Hungary', 'Ukraine', 'Greece', 'Romania', 'Singapore',
    'Chile', 'Colombia', 'Peru', 'Morocco', 'Kenya', 'Ghana', 'Venezuela', 'Qatar',
    'Kuwait', 'Iraq', 'Iran', 'Syria', 'Lebanon', 'Jordan', 'New Guinea', 'Kazakhstan'
    // Add more as needed
  ];

  String? _selectedCountry;

  @override
  Widget build(BuildContext context) {
    return DropdownButtonFormField<String>(
      decoration: InputDecoration(
        labelText: 'Select Country',
        border: OutlineInputBorder(),
      ),
      value: _selectedCountry,
      items: _countries.map((country) {
        return DropdownMenuItem(
          value: country,
          child: Text(country),
        );
      }).toList(),
      onChanged: (value) {
        setState(() {
          _selectedCountry = value;
        });
      },
      validator: (value) => value == null ? 'Please select a country' : null,
    );
  }
}