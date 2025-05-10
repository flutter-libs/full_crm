import 'package:flutter/material.dart';
import 'package:frontend/constants/form_fields.dart';

class USStateSelector extends StatefulWidget {
  const USStateSelector({super.key});

  @override
  State<USStateSelector> createState() => _USStateSelectorState();
}

class _USStateSelectorState extends State<USStateSelector> {
  final List<String> _states = [
    'Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California', 'Colorado', 'Connecticut',
    'Delaware', 'Florida', 'Georgia', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa',
    'Kansas', 'Kentucky', 'Louisiana', 'Maine', 'Maryland', 'Massachusetts', 'Michigan',
    'Minnesota', 'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire',
    'New Jersey', 'New Mexico', 'New York', 'North Carolina', 'North Dakota', 'Ohio',
    'Oklahoma', 'Oregon', 'Pennsylvania', 'Rhode Island', 'South Carolina', 'South Dakota',
    'Tennessee', 'Texas', 'Utah', 'Vermont', 'Virginia', 'Washington', 'West Virginia',
    'Wisconsin', 'Wyoming'
  ];

  String? _selectedState;

  @override
  Widget build(BuildContext context) {
    return DropdownButtonFormField<String>(
      decoration: showFormFieldStyle('Select State'),
      value: _selectedState,
      items: _states.map((state) {
        return DropdownMenuItem(
          value: state,
          child: Text(state),
        );
      }).toList(),
      onChanged: (value) {
        setState(() {
          _selectedState = value;
        });
      },
      validator: (value) => value == null ? 'Please select a state' : null,
    );
  }
}