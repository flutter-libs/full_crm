import 'dart:convert';
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';

class ImageUploadWidget extends StatefulWidget {
  final Function(String) onImageSelected;

  const ImageUploadWidget({super.key, required this.onImageSelected});

  @override
  State<ImageUploadWidget> createState() => _ImageUploadWidgetState();
}

class _ImageUploadWidgetState extends State<ImageUploadWidget> {
  File? _image;
  String? _base64Image;
  final picker = ImagePicker();
  Future<void> _pickImage() async {
    final pickedFile = await picker.pickImage(source: ImageSource.gallery);
    if (pickedFile != null) {
      setState(() {
        _image = File(pickedFile.path);
      });
      _convertImageToBase64(_image!);
    }
  }

  Future<void> _convertImageToBase64(File imageFile) async {
    final bytes = await imageFile.readAsBytes();
    final base64Image = base64Encode(bytes);

    setState(() {
      _base64Image = base64Image;
    });

    widget.onImageSelected(_base64Image!);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // Display the selected image
        _image != null
            ? Image.file(_image!)
            : Text('No image selected', style: TextStyle(fontSize: 16)),

        SizedBox(height: 20),

        ElevatedButton(
          onPressed: _pickImage,
          child: Text('Select Image'),
        ),
      ],
    );
  }
}