document.addEventListener('DOMContentLoaded', function () {
    // Action buttons functionality
    document.querySelectorAll('.action-btn').forEach(button => {
        button.addEventListener('click', function () {
            const action = this.querySelector('span:last-child').textContent;
            console.log('Acción seleccionada:', action);

            // Visual feedback mejorado
            this.style.transform = 'translateX(8px) scale(0.98)';
            setTimeout(() => {
                this.style.transform = '';
            }, 200);
        });
    });

    // Logout functionality
    document.querySelector('.logout-btn').addEventListener('click', function (e) {
        e.preventDefault();
        if (confirm('¿Estás seguro de que deseas cerrar sesión?')) {
            console.log('Cerrando sesión...');
        }
    });
});

// Función para agregar nuevas secciones dinámicamente
function addNewSection() {
    const sectionName = prompt('Ingrese el nombre de la nueva sección:');
    if (!sectionName) return;

    const sectionsGrid = document.getElementById('sectionsGrid');
    const addButton = document.querySelector('.add-section-btn');

    // Iconos disponibles para nuevas secciones
    const icons = ['🚀', '📋', '🎯', '💡', '🔍', '📞', '🌟', '🎨', '⚡', '🎪'];
    const randomIcon = icons[Math.floor(Math.random() * icons.length)];

    const newSection = document.createElement('div');
    newSection.className = 'section-box';
    newSection.innerHTML = `
        <div class="section-header">
            <div class="section-title">
                <span class="section-title-icon">${randomIcon}</span>
                <span>${sectionName}</span>
                <button onclick="removeSection(this)" style="margin-left: auto; background: none; border: none; color: #e53e3e; cursor: pointer; font-size: 1.2rem;" title="Eliminar sección">🗑️</button>
            </div>
        </div>
        <div class="section-actions">
            <button class="action-btn" onclick="addAction(this)">
                <span class="action-icon">➕</span>
                <span>Agregar nueva acción</span>
            </button>
        </div>
    `;

    // Insertar antes del botón de agregar
    sectionsGrid.insertBefore(newSection, addButton);

    // Agregar efecto de aparición
    newSection.style.opacity = '0';
    newSection.style.transform = 'translateY(20px)';
    setTimeout(() => {
        newSection.style.transition = 'all 0.5s ease';
        newSection.style.opacity = '1';
        newSection.style.transform = 'translateY(0)';
    }, 100);
}

// Función para agregar acciones a una sección
function addAction(button) {
    const actionName = prompt('Ingrese el nombre de la nueva acción:');
    if (!actionName) return;

    const actionsContainer = button.parentElement;
    const icons = ['📝', '🔧', '📊', '💾', '🔍', '📞', '📧', '⚙️', '🎯', '✅'];
    const randomIcon = icons[Math.floor(Math.random() * icons.length)];

    const newAction = document.createElement('button');
    newAction.className = 'action-btn';
    newAction.innerHTML = `
        <span class="action-icon">${randomIcon}</span>
        <span>${actionName}</span>
        <button onclick="removeAction(this)" style="margin-left: auto; background: none; border: none; color: #e53e3e; cursor: pointer; opacity: 0.7;" title="Eliminar acción">✕</button>
    `;

    // Agregar funcionalidad de click
    newAction.addEventListener('click', function (e) {
        if (e.target.textContent !== '✕') {
            console.log('Nueva acción seleccionada:', actionName);
            this.style.transform = 'translateX(8px) scale(0.98)';
            setTimeout(() => {
                this.style.transform = '';
            }, 200);
        }
    });

    actionsContainer.insertBefore(newAction, button);
}

// Función para eliminar una sección
function removeSection(button) {
    if (confirm('¿Está seguro de que desea eliminar esta sección?')) {
        const section = button.closest('.section-box');
        section.classList.add('removing');
        setTimeout(() => {
            section.remove();
        }, 400);
    }
}

// Función para eliminar una acción
function removeAction(button) {
    const action = button.closest('.action-btn');
    action.style.animation = 'fadeOutDown 0.3s ease forwards';
    setTimeout(() => {
        action.remove();
    }, 300);
}