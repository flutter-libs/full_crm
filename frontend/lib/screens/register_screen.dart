import 'package:flutter/material.dart';
import 'package:frontend/models/User.dart';
import 'package:frontend/screens/dashboard_screen.dart';
import 'package:frontend/screens/home_screen.dart';
import 'package:frontend/services/UserAPIService.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;

class RegisterScreen extends StatefulWidget {
  const RegisterScreen({super.key});
  static const String id = "register";
  @override
  State<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  final _formKey = GlobalKey<FormState>();
  final _userService = UserAPIService();
  var _obscurePassword = true;
  var _obscureConfirmPassword = true;
  DateTime? _selectedDateOfBirth;
  DateTime _dateCreated = DateTime.now();
  // Controllers for form fields
  final TextEditingController _email = TextEditingController();
  final TextEditingController _password = TextEditingController();
  final TextEditingController _confirmPassword = TextEditingController();
  final TextEditingController _userName = TextEditingController();
  final TextEditingController _name = TextEditingController();
  final TextEditingController _address = TextEditingController();
  final TextEditingController _city = TextEditingController();
  final TextEditingController _state = TextEditingController();
  final TextEditingController _zipCode = TextEditingController();
  final TextEditingController _description = TextEditingController();

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
        dateOfBirth: _selectedDateOfBirth?.toIso8601String(),
        dateCreated: DateTime.now().toIso8601String(),
      );

      final result = await _userService.register(user);
      if(!mounted) return;
      if (result != null) {
        alert.showSuccessToast(context, 'Registration successful, now able to access your dashboard and profile', 'Registration Successful');
        Navigator.pushReplacement(
          context,
          MaterialPageRoute(builder: (_) => const DashboardScreen()),
        );
      } else {
        alert.showErrorToast(context, 'Registration failed, please try again later.', 'Registration Failed');
      }
    }
  }

  void _pickDate() async {
    DateTime? picked = await showDatePicker(
        context: context,
        barrierColor: Color.fromRGBO(22, 22, 22, 0.5),
        helpText: 'Select Your Date of Birth',
        cancelText: 'Cancel',
        confirmText: 'Submit',
        fieldHintText: 'MM/DD/YYYY',
        fieldLabelText: 'Enter date manually',
        errorFormatText: 'Invalid format',
        errorInvalidText: 'Out of range',
        initialDate: DateTime.now(),
        firstDate: DateTime(1900, 1, 1),
        lastDate: DateTime(2200, 12, 31),
        builder: (BuildContext context, Widget? child) {
          return Theme(
            data: ThemeData.dark().copyWith(
              colorScheme: ColorScheme.dark(
                primary: Colors.black38,
                onPrimary: Colors.indigoAccent,
                onSurface: Colors.indigoAccent,
              ),
              textButtonTheme: TextButtonThemeData(
                style: TextButton.styleFrom(
                  foregroundColor: Colors.white,
                  backgroundColor: Color.fromRGBO(22, 22, 22, 1),
                ),
              ),
              datePickerTheme: DatePickerThemeData(
                dayStyle: TextStyle(fontWeight: FontWeight.bold, color: Colors.white),
                yearStyle: TextStyle(fontWeight: FontWeight.bold, color: Colors.white),
                weekdayStyle: TextStyle(fontWeight: FontWeight.bold, color: Colors.indigoAccent),
              ),
              canvasColor: Theme.of(context).primaryColorDark,
            ), child: child!,

          );
        }
    );
    if (picked != null) {
      setState(() => _selectedDateOfBirth = picked);
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
          child: Card(
            elevation: 10.0,
            shadowColor: Color.fromRGBO(33, 33, 33, 1),
            child: Padding(padding: EdgeInsets.all(20.0),
              child: Column(children: [
            TextFormField(controller: _userName, decoration: const InputDecoration(labelText: "Username")),
            TextFormField(controller: _email, decoration: const InputDecoration(labelText: "Email")),
            TextFormField(controller: _name, decoration: const InputDecoration(labelText: "Full Name")),
            TextFormField(controller: _address, decoration: const InputDecoration(labelText: "Address")),
            TextFormField(controller: _city, decoration: const InputDecoration(labelText: "City")),
            TextFormField(controller: _state, decoration: const InputDecoration(labelText: "State")),
            TextFormField(controller: _zipCode, decoration: const InputDecoration(labelText: "Zip Code")),
            TextFormField(
                controller: _description,
                decoration: InputDecoration(
                  labelText: 'Description',
                  enabledBorder: UnderlineInputBorder(
                    borderSide: BorderSide(color: Colors.cyan),
                  ),
                  focusedBorder: UnderlineInputBorder(
                    borderSide: BorderSide(color: Colors.blue, width: 2),
                  ),
                  errorBorder: UnderlineInputBorder(
                    borderSide: BorderSide(color: Colors.red),
                  ),
                ),
            ),
            TextField(
              onTap: () => _pickDate(),
              decoration: const InputDecoration(labelText: "Date of Birth (YYYY-MM-DD)"),
            ),
                TextFormField(
                    controller: _password,
                    decoration: InputDecoration(
                      labelText: 'Password',
                      enabledBorder: UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.cyan),
                      ),
                      focusedBorder: UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.blue, width: 2),
                      ),
                      errorBorder: UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.red),
                      ),
                      suffixIcon: IconButton(
                        icon:
                        _obscurePassword
                            ? Icon(
                          Icons.visibility_off,
                          color: Colors.green,
                          size: 16.0,
                        )
                            : Icon(
                          Icons.visibility,
                          color: Colors.red,
                          size: 16.0,
                        ),
                        onPressed: () {
                          setState(() {
                            _obscurePassword =
                            !_obscurePassword;
                          });
                        },
                      ),
                    ), obscureText: _obscurePassword),
                TextFormField(
                    controller: _confirmPassword,
                    decoration: InputDecoration(
                      labelText: 'Confirm Password',
                      enabledBorder: UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.cyan),
                      ),
                      focusedBorder: UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.blue, width: 2),
                      ),
                      errorBorder: UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.red),
                      ),
                      suffixIcon: IconButton(
                        icon:
                        _obscureConfirmPassword
                            ? Icon(
                          Icons.visibility_off,
                          color: Colors.green,
                          size: 16.0,
                        )
                            : Icon(
                          Icons.visibility,
                          color: Colors.red,
                          size: 16.0,
                        ),
                        onPressed: () {
                          setState(() {
                            _obscureConfirmPassword =
                            !_obscureConfirmPassword;
                          });
                        },
                      ),
                    ),
                    obscureText: _obscureConfirmPassword
                ),
            const SizedBox(height: 20),
            ElevatedButton(
                onPressed: _register,
                style: ElevatedButton.styleFrom(
                  foregroundColor: Colors.white,
                  backgroundColor: Colors.indigo,
                  shadowColor: Color.fromRGBO(77, 77, 77, 1),
                  elevation: 5.0,
                  padding: EdgeInsets.symmetric(vertical: 10.0, horizontal: 20.0),
                ),
                child: const Text(
                  "Register",
                  style: TextStyle(
                    color: Colors.white,
                    fontFamily: "Ubuntu",
                    fontWeight: FontWeight.w700,
                    fontSize: 22.0
                  ),
                ),
            ),
          ]),
        ),
      ),
      ),
      ),
    );
  }
}