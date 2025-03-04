namespace Application.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Dominio.Entities;
    using Application.DTOs.Login;

    public static class ModuloExtensions
    {
        public static List<ModuloDto> ToModuloGeneralDtoList(this IEnumerable<ModuloGeneral> modulos)
        {
            List<ModuloDto>? listaModulos = null;
            if (modulos != null && modulos.Any())
            {
                listaModulos = new List<ModuloDto>();
                List<ModuloGeneral> subModulos = modulos.Where(x => !string.IsNullOrEmpty(x.IdMenuCatalogo)).ToList();
                foreach (var modulo in modulos)
                {

                    if (string.IsNullOrEmpty(modulo.IdMenuCatalogo))
                    {
                        ModuloDto mod = new ModuloDto();
                        mod.Label = modulo.Menu;
                        mod.Icono = modulo.Icono;
                        mod.Ruta = modulo.Ruta;
                        var subM = subModulos.Where(x => Convert.ToInt16(x.IdMenuCatalogo) == modulo.IdMenu).ToList();
                        if (subM.Any())
                        {
                            List<ModuloDto> listaSubModulos = new List<ModuloDto>();
                            foreach (var subModulo in subM)
                            {
                                ModuloDto subMod = new ModuloDto();
                                subMod.Label = subModulo.Menu;
                                subMod.Icono = subModulo.Icono;
                                subMod.Ruta = subModulo.Ruta;
                                listaSubModulos.Add(subMod);
                            }
                            mod.Submodulos = listaSubModulos;
                        }
                        listaModulos.Add(mod);
                    }
                }
            }
            return listaModulos;
        }
    }
}