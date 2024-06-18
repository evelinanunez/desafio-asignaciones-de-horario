Condiciones para la resolución:
- Modelar una solución Orientada a Objetos
- Se recomienda desarrollarlo usando TDD
- No es necesario implementar persistencia, ni interfaz de usuario. Alcanza con los
	tests automatizados necesarios para verificar que la funcionalidad está
	correctamente implementada.
- Subir el código a un repositorio privado o enviarlo adjunto por mail en un zip.
- Asignaciones de horarios


Últimamente estamos teniendo problemas de staffing en nuestras sucursales, nosotros
siempre tuvimos como política: horarios flexibles a medida que la persona lo necesite.
Esto siempre lo logramos respetando las disponibilidades de nuestros empleados.

También con el pasar del tiempo nos dimos cuenta de que hay personas que funcionan
mejor como un equipo y una vez que logramos armar equipo tomamos como política:
equipo que funciona no se toca. 

Con lo cual los equipos se asignan en conjunto o no se asignan en absoluto. 

La disponibilidad de un equipo es la consecuencia de las disponibilidades de sus integrantes, es decir un equipo puede cubrir el turno de un día determinado (por ejemplo, el 02/01/2019) si y sólo si todos sus integrantes pueden.

El tema es que estamos creciendo, y lo que antes era simple porque éramos pocos y nos
conocíamos, ahora ya no lo es y se cometen muchos errores a la hora de asignar los
horarios. 

Ya perdimos varias personas clave para nosotros por esto y no puede seguir
pasando así que necesitamos que nos ayudes a desarrollar un sistema que automatice la
respuesta a la pregunta ¿Quiénes estarían disponibles para cubrir la asignación?

Después de buscar varios ejemplos de disponibilidades notamos que muchas veces se cae
en los siguientes patrones:

- Fines de semana: Sábado y Domingo
- Entre semana Lunes a Viernes
- Día puntual (Martes, o Jueves)
- Día del mes (1, 5 ó 10)

También, las disponibilidades suelen ser combinaciones de todas las situaciones anteriores,
por ejemplo:

- Margarita puede trabajar los Miércoles y los fines de semana, una disponibilidad
	combinada de día puntual y fin de semana
- Gregorio y Esteban pueden trabajar solamente los fines de semana
- Jazmín tiene disponibilidad de Lunes a Viernes, que se considera la disponibilidad
	default
- Fanny y Benicio trabajan todos los jueves y los días 2, 3, 5, 7 y del 20 al 28, es decir
	una combinación de días puntuales y días del mes

