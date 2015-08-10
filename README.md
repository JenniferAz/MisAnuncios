# MisAnuncios

##INTRODUCCIÓN

MisAnuncios en una aplicación web programada utilizando generalmente ASP .NET MVC para el backend y HTML5 y JavaScript para el frontend. Dispone de una base de datos local SQL Server Express que contiene tablas para autentificación de usuarios (incluyendo roles), anuncios y categorías.

##FUNCIONALIDADES

La aplicación permite a cualquier usuario:

* Ver la información general del sitio.
* Ver el listado de anuncios publicados.
* Buscar anuncios en una ciudad y además filtrar por categoría.
* Registrarse e iniciar sesión.

Un usuario registrado y con su sesión iniciada además tiene la posibilidad de:

* Cambiar sus datos: nombre y apellidos, ciudad, email y contraseña.
* Añadir anuncios únicamente en su ciudad.

El administrador es el encargado de generar nuevas categorías para los anuncios. Sólo él tiene acceso a esta funcionalidad junto con la mencionada anteriormente.

Para probar la aplicación como usuario registrado, hay un usuario creado con los siguientes datos para el inicio de sesión:

* Email: usuario@user.com
* Contraseña: User_1
aunque también puede registrarse un nuevo usuario con los datos deseados.

Los datos para iniciar sesión como administrador y acceder a la sección de categorías son:

* Email: admin@admin.com
* Contraseña: Admin_1

##DESCRIPCIÓN

A continuación se desarrolla una descripción general de cada vista y se justifican algunas de las decisiones llevadas a cabo en la implementación y el maquetado del front end.

Todas las vistas comparten el mismo menú y el footer por consistencia y en cada una se muestra activo el link correspondiente para dar a conocer al usuario dónde se encuentra en cada momento.
    
La sección de inicio (Mis anuncios) incluye la presentación de la web y la zona para iniciar sesión y registrarse. Si alguna de estas dos opciones es fallida, se redirige a otra vista específica para ella donde se indican los errores en el formulario. Una vez iniciada la sesión, en la barra de menú aparecerá un saludo con el nombre de usuario (en este caso el email) y la opción para cerrar la misma y en la página de inicio desaparecerán los formularios de inicio de sesión y registro. Es posible acceder a los datos de registro pulsando en el link del menú donde aparece el nombre de usuario, el cual mostrará una vista con los campos necesarios a rellenar. Una vez guardados, se redirigirá a inicio y se verá el nombre de usuario cambiado. Si no se deseara cambiar el perfil, se puede pulsar el botón cancelar que también redirigirá a inicio pero el email se mostrará tal cual estaba. A su vez, se activará la opción de crear anuncio en la sección anuncios. 
    
Dicha vista muestra la lista completa de anuncios y un buscador por ciudad y categoría. Si el usuario ha iniciado sesión el campo de ciudad automáticamente se rellenará con el nombre de su ciudad para permitirle buscar anuncios de la categoría que elija en su ciudad. En la lista de categorías, cabe destacar que no aparecen siempre todas las categorías existentes en la base de datos, sino aquellas para las que hay anuncios para facilitarle la búsqueda al usuario.  Si no hay resultados, aparecerá un mensaje de alerta y mostrará la lista completa de anuncios como alternativa. Al seleccionar la opción de agregar un anuncio, se muestra otra vista con el formulario correspondiente en el que el campo ciudad no puede modificarse dado que sólo se permite al usuario crear anuncios en su ciudad. Las áreas de texto para el título y el contenido son ampliables por el usuario, ya que se espera que su contenido sea más extenso y esto lo hará más fácil de leer al insertarlo. Si un anuncio se crea satisfactoriamente, se muestra un mensaje y se vuelve a la tabla de anuncios, donde aparecerá.
    
La sección buscador se corresponde con la API REST. Al principio muestra una tabla vacía que se llenará al insertar el nombre de una ciudad y presionar el botón buscar. Si no hay resultados, simplemente aparecerá una alerta. EL controlador para esta vista es el único que se ha desarrollado utilizando Web Api e incluye una lista de métodos para devolver todos los anuncios de la base de datos, devolver un anuncio por id, devolver un anuncio por ciudad (que es lo que permite la vista y la funcionalidad requerida) y crear, actualizar y borrar un anuncio.
    
Por último, el administrador tiene acceso a la gestión de categorías, una opción en el menú que se muestra exclusivamente a ese rol. En esa vista se visualiza la lista de categorías y la opción de crear una nueva. Al seleccionar esta acción se abrirá la vista con un formulario para nombrar a la nueva categoría. Cuando ésta se haya creado, aparecerá un mensaje de éxito y volverá a la lista de las categorías.

Cabe mencionar que se ha querido prestar atención a la experiencia de usuario añadiendo formas de retroalimentación y algunos aspectos para mejorar la usabilidad del sistema. Los formularios, tanto los de registro e inicio de sesión como los de creación de anuncios y categorías, proporcionan mensajes de error para cada campo en caso de que el contenido no tenga el formato correcto y los que se consideran necesarios son extensibles. Así mismo, como se ha explicado en el párrafo anterior, cuando un anuncio o una categoría se añaden, aparece un mensaje de alerta para indicar que se hizo correctamente y siempre se le proporciona opción de retroceso al usuario. En el caso de la búsqueda de anuncios, también aparece un mensaje informativo si no se encuentran resultados.

