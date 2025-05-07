import 'package:flutter/material.dart';

class CustomAppBar extends StatelessWidget implements PreferredSizeWidget {
  final String title;
  final bool showBackButton;
  final List<Widget>? actions;

  const CustomAppBar({
    super.key,
    required this.title,
    this.showBackButton = false,
    this.actions,
  });

  @override
  Widget build(BuildContext context) {
    return AppBar(
      automaticallyImplyLeading: showBackButton,
      title: Text(
        title,
        textAlign: TextAlign.center,
        style: TextStyle(
          fontFamily: "Ubuntu-Bold",
          fontSize: 24.0,
          fontWeight: FontWeight.w700,
          color: Colors.white,
        ),
      ),
      leading: Builder(
        builder: (context) => IconButton(
          icon: const Icon(Icons.menu, color: Colors.white,),
          onPressed: () => Scaffold.of(context).openDrawer(),
        ),
      ),
      backgroundColor: Colors.indigo,
      centerTitle: true,
      actions: actions,
    );
  }

  @override
  Size get preferredSize => const Size.fromHeight(kToolbarHeight);
}