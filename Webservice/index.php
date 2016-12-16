<?php

  $required = [
    './vendor/autoload'
  ];

  foreach ($required as $file) {
    include_once($file.'.php');
  }

  use \Slim\App as App;
  use \Psr\Http\Message\ServerRequestInterface as Request;
  use \Psr\Http\Message\ServerResponseInterface as Response;

  $app = new App();
  $app->get('/', function() {
    echo "It works !";
  });
  $app->run();

?>
