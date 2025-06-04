<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Color extends Model
{
    protected $fillable = ['hex', 'name', 'css'];
    public $timestamps = false;
}
