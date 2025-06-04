<?php

use App\Http\Controllers\ColorController;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

Route::apiResource('colors', ColorController::class);

Route::get('colors/hex/{hex}', [ColorController::class, 'findByHex']);
Route::get('colors/name/{name}', [ColorController::class, 'findByName']);