import 'package:flutter/material.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/side_nav_drawer.dart';

class DashboardScreen extends StatelessWidget {
  const DashboardScreen({super.key});
  static const String id = "dashboard_screen";
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: CustomAppBar(title: 'Dashboard'),
      drawer: SideNavDrawer(),
      body: SingleChildScrollView(
        child: Column(
          children: List.generate(5, (index) {
            return Padding(
              padding: const EdgeInsets.all(8.0),
              child: DashboardRow(rowIndex: index),
            );
          }),
        ),
      ),
    );
  }
}

class DashboardRow extends StatelessWidget {
  final int rowIndex;

  const DashboardRow({super.key, required this.rowIndex});

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Text(
          'Row $rowIndex',
          style: Theme.of(context).textTheme.headlineLarge,
        ),
        SizedBox(
          height: 200, // Adjust this value as needed
          child: ListView(
            scrollDirection: Axis.horizontal,
            children: List.generate(5, (index) {
              return Padding(
                padding: const EdgeInsets.all(8.0),
                child: Card(
                  elevation: 5,
                  child: Container(
                    width: 250, // Width of each card
                    height: 150, // Height of each card
                    color: Colors.blueAccent,
                    child: Center(
                      child: Text(
                        'Card $index',
                        style: Theme.of(context).textTheme.bodyMedium,
                      ),
                    ),
                  ),
                ),
              );
            }),
          ),
        ),
      ],
    );
  }
}
