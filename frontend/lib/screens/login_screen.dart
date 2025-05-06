import 'package:flutter/material.dart';
import 'package:frontend/constants/form_fields.dart';
import 'package:frontend/screens/dashboard_screen.dart';
import 'package:frontend/screens/register_screen.dart';
import 'package:frontend/services/user_api_service.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/side_nav_drawer.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  static const String id = "login_screen";

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  final _formKey = GlobalKey<FormState>();
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  final UserAPIService _apiService = UserAPIService();
  var _obscurePassword = true;

  bool _isLoading = false;

  Future<void> _login() async {
    if (_formKey.currentState?.validate() != true) return;

    setState(() {
      _isLoading = true;
    });

    final success = await _apiService.login(
      _emailController.text.trim(),
      _passwordController.text.trim(),
    );

    setState(() {
      _isLoading = false;
    });

    if (success && context.mounted) {
      Navigator.pushNamed(context, DashboardScreen.id);
    } else {
      // Error already shown by alert service
    }
  }

  @override
  Widget build(BuildContext context) {
    double screenWidth = MediaQuery.of(context).size.width;
    double heightForm = screenWidth > 600 ? 500 : 300;
    double widthForm = screenWidth > 600 ? 600 : 300;
    double sepWidth = widthForm == 600 ? 230 : 110;
    return Scaffold(
      appBar: CustomAppBar(title: 'CRM: Sign In', showBackButton: false),
      drawer: SideNavDrawer(),
      body: Center(
        child: SizedBox(
          width: widthForm,
          height: heightForm,
          child: SingleChildScrollView(
            child: Card(
              elevation: 10.0,
              shadowColor: Color.fromRGBO(33, 33, 33, 1),
              child: Column(
                children: [
                  Row(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      SizedBox(height: 30.0),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: Text(
                          'Login',
                          textAlign: TextAlign.right,
                          style: TextStyle(
                            color: Colors.indigoAccent,
                            fontSize: 35.0,
                            fontFamily: 'Ubuntu',
                            fontWeight: FontWeight.w700,
                          ),
                        ),
                      ),
                      SizedBox(width: sepWidth),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: TextButton(
                          onPressed: () {
                            Navigator.pushNamed(context, RegisterScreen.id);
                          },
                          child: Text(
                            'Register instead?',
                            textAlign: TextAlign.center,
                            style: TextStyle(
                              color: Colors.green,
                              fontWeight: FontWeight.normal,
                              fontFamily: 'Ubuntu',
                              fontSize: 16.0,
                            ),
                          ),
                        ),
                      ),
                    ],
                  ),
                  Form(
                    key: _formKey,
                    child: Padding(
                      padding: EdgeInsets.all(20.0),
                      child: Column(
                        children: [
                          Padding(
                            padding: const EdgeInsets.all(8.0),
                            child: TextFormField(
                              controller: _emailController,
                              decoration: kTextInputEmailStyle,
                              keyboardType: TextInputType.emailAddress,
                              validator:
                                  (val) =>
                                      val == null || val.isEmpty
                                          ? 'Email is required'
                                          : null,
                            ),
                          ),
                          const SizedBox(height: 16),
                          Padding(
                            padding: const EdgeInsets.all(8.0),
                            child: TextFormField(
                              controller: _passwordController,
                              validator:
                                  (val) =>
                                      val == null || val.length < 6
                                          ? 'Minimum 6 characters'
                                          : null,
                              decoration: InputDecoration(
                                label: Text(
                                  'Password',
                                  style: TextStyle(
                                    color: Colors.white,
                                    fontFamily: 'Ubuntu',
                                    fontWeight: FontWeight.bold,
                                    fontSize: 14.0,
                                  ),
                                ),
                                enabledBorder: UnderlineInputBorder(
                                  borderSide: BorderSide(color: Colors.cyan),
                                ),
                                focusedBorder: UnderlineInputBorder(
                                  borderSide: BorderSide(
                                    color: Colors.blue,
                                    width: 2,
                                  ),
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
                                      _obscurePassword = !_obscurePassword;
                                    });
                                  },
                                ),
                              ),
                              keyboardType: TextInputType.text,
                              obscureText: _obscurePassword,
                            ),
                          ),
                          const SizedBox(height: 24),
                          _isLoading
                              ? const CircularProgressIndicator()
                              : Padding(
                                padding: const EdgeInsets.all(8.0),
                                child: ElevatedButton(
                                  onPressed: _login,
                                  style: ElevatedButton.styleFrom(
                                    foregroundColor: Colors.white,
                                    backgroundColor: Colors.indigo,
                                    shadowColor: Color.fromRGBO(77, 77, 77, 1),
                                    elevation: 5.0,
                                    padding: EdgeInsets.symmetric(
                                      vertical: 10.0,
                                      horizontal: 20.0,
                                    ),
                                  ),
                                  child: Padding(
                                    padding: const EdgeInsets.all(8.0),
                                    child: Text(
                                      "Login",
                                      textAlign: TextAlign.center,
                                      style: TextStyle(
                                        color: Colors.white,
                                        fontFamily: "Ubuntu",
                                        fontWeight: FontWeight.w700,
                                        fontSize: 22.0,
                                      ),
                                    ),
                                  ),
                                ),
                              ),
                          const SizedBox(height: 16),
                        ],
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
