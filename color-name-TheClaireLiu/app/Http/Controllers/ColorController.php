<?php

namespace App\Http\Controllers;

use App\Models\Color;
use Illuminate\Http\Request;

class ColorController extends Controller
{
    //retrieve all colors
    public function index(){
        return response()->json(Color::all());
    }

    // retrieve single color
    public function show($id){
        return response()->json(Color::findOrFail($id));
    }

    // create a new color
    public function store(Request $request){
        $color = new Color();
        $request->validate([
            'name' => 'required',
            'hex' => 'required',
            'css' => 'required',
        ]);
        $color->name = $request->name;
        $color->hex = $request->hex;
        $color->css = $request->css;

        $color->save();
        return response()->json($color, 201);
    }

    // update a color
    public function update(int $id, Request $request){
        $color = Color::find($id);

        if(is_null($color)){
            return response()->json(['error' => 'Color not found'], 404);
        }

        if(isset($request->name)){
            $color->name = $request->name;
        }
        if(isset($request->hex)){
            $color->hex = $request->hex;
        }
        if(isset($request->css)){
            $color->css = $request->css;
        }

        $color->save();
        return response()->json($color);
    }

    // delete a color
    public function destroy(int $id){
        $color = Color::findOrFail($id);
        $color->delete();

        return response()->json(['message'=> 'Color deleted']);
    }

    // returns all colors by hex value
    public function findByHex($hex){

        return Color::where('hex', $hex) -> get();
    }

    // returns all colors by name value
    public function findByName($name){

        return Color::where('name', 'LIKE', "%{$name}%") -> get();
    }

}
