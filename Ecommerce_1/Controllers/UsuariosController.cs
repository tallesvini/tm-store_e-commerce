using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce_1.Data;
using Ecommerce_1.Models;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Ecommerce_1.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly Contexto _context;
        private readonly Usuario _usuario;

        public UsuariosController(Contexto context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {

            if(User.Identity.IsAuthenticated)
            {
                return _context.Usuario != null ?
                          View(await _context.Usuario.ToListAsync()) :
                          Problem("Entity set 'Contexto.Usuario'  is null.");
            }
            else
            {
                return RedirectToAction("Login", "Usuarios");
            }            

        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Celular,Email, Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var senhaUser = usuario.Senha;
                Hash hash = new Hash(SHA512.Create());
                string senhaEncrip = hash.CriptografarSenha(senhaUser);

                usuario.Senha = senhaEncrip;

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(String email, String senha, bool manterLogado)
        {
            var emailAddUser = email;
            var senhaAddUser = senha;
            Hash hash = new Hash(SHA512.Create());
            string senhaCripto = hash.CriptografarSenha(senhaAddUser);


            try
            {
                List<Claim> direitoAcessos = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,emailAddUser.ToString()),
                    new Claim(ClaimTypes.Name, senhaCripto)
                };

                var identity = new ClaimsIdentity(direitoAcessos, "Identity.Login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });


                var userBD = _context.Usuario.FirstOrDefault(m => m.Email == email);

                if(userBD != null)
                {
                    var senhaUsuarioBD = userBD.Senha;

                    if(senhaUsuarioBD == senhaCripto)
                    {
                        await HttpContext.SignInAsync(userPrincipal,
                            new AuthenticationProperties
                            {
                                IsPersistent = manterLogado,
                                ExpiresUtc = DateTime.Now.AddHours(1)
                            });

                        ViewBag.Message = string.Format("logado!");
                    }
                    else
                    {
                        ViewBag.ErrorUser = string.Format("Não logado!");
                    }
                }     
                else
                {
                    ViewBag.ErrorUser = string.Format("Usuário não encontrado!");
                }
            }

            catch (Exception ex)
            {
                throw;
            }

            return View();

        }

        public void GetUser()
        {
            

            var a = HttpContext.User.Identity.Name;

        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Celular,Email")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'Contexto.Usuario'  is null.");
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuario?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
