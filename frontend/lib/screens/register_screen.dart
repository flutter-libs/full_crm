import 'package:flutter/material.dart';
import 'package:frontend/models/User.dart';
import 'package:frontend/screens/dashboard_screen.dart';
import 'package:frontend/screens/home_screen.dart';
import 'package:frontend/services/UserAPIService.dart';

class RegisterScreen extends StatefulWidget {
  const RegisterScreen({super.key});
  static const String id = "register";
  @override
  State<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  final _formKey = GlobalKey<FormState>();
  final _userService = UserAPIService();

  // Controllers for form fields
  final TextEditingController _email = TextEditingController();
  final TextEditingController _password = TextEditingController();
  final TextEditingController _userName = TextEditingController();
  final TextEditingController _name = TextEditingController();
  final TextEditingController _address = TextEditingController();
  final TextEditingController _city = TextEditingController();
  final TextEditingController _state = TextEditingController();
  final TextEditingController _zipCode = TextEditingController();
  final TextEditingController _description = TextEditingController();
  final TextEditingController _dateOfBirth = TextEditingController();

  void _register() async {
    if (_formKey.currentState!.validate()) {
      final user = User(
        userName: _userName.text,
        email: _email.text,
        name: _name.text,
        address: _address.text,
        city: _city.text,
        state: _state.text,
        zipCode: _zipCode.text,
        password: _password.text,
        description: _description.text,
        dateOfBirth: _dateOfBirth.text,
        dateCreated: DateTime.now().toIso8601String(),
      );

      final result = await _userService.register(user);
      if (result != null) {
        Navigator.pushReplacement(
          context,
          MaterialPageRoute(builder: (_) => const DashboardScreen()),
        );
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Registration failed')),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Register")),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Form(
          key: _formKey,
          child: Column(children: [
            TextFormField(controller: _userName, decoration: const InputDecoration(labelText: "Username")),
            TextFormField(controller: _email, decoration: const InputDecoration(labelText: "Email")),
            TextFormField(controller: _password, decoration: const InputDecoration(labelText: "Password"), obscureText: true),
            TextFormField(controller: _name, decoration: const InputDecoration(labelText: "Full Name")),
            TextFormField(controller: _address, decoration: const InputDecoration(labelText: "Address")),
            TextFormField(controller: _city, decoration: const InputDecoration(labelText: "City")),
            TextFormField(controller: _state, decoration: const InputDecoration(labelText: "State")),
            TextFormField(controller: _zipCode, decoration: const InputDecoration(labelText: "Zip Code")),
            TextFormField(controller: _description, decoration: const InputDecoration(labelText: "Description")),
            TextFormField(
              controller: _dateOfBirth,
              decoration: const InputDecoration(labelText: "Date of Birth (YYYY-MM-DD)"),
            ),
            const SizedBox(height: 20),
            ElevatedButton(onPressed: _register, child: const Text("Register")),
          ]),
        ),
      ),
    );
  }
}