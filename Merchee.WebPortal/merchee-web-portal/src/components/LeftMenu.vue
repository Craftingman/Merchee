<template>
<div class="left-menu">
    <ul>
    <li v-for="(item, index) in menuItems" :key="index" :class="{ active: item.active }">
        <a :href="item.url">
        <i :class="item.icon"></i>
        <span>{{ item.label }}</span>
        </a>
    </li>
    </ul>
</div>
</template>

<script>
export default {
data() {
    return {
    menuItems: [
        { label: 'Company', url: '/', icon: 'fa fa-home', active: true },
        { label: 'Products', url: '/products', icon: 'fa fa-cube', active: false },
        { label: 'Shelves', url: '/shelves', icon: 'fa fa-shopping-cart', active: false },
        { label: 'Customer Actions', url: '/customerActions', icon: 'fa fa-cog', active: false }
    ]
    };
}
};
</script>

<style>
.left-menu {
    display: inline-flex;
    width: 200px;
    background-color: #f2f2f2;
    height: 100%;
}

.left-menu ul {
    width: 100%;
list-style: none;
padding: 0;
margin: 0;
}

.left-menu li {
padding: 10px;
cursor: pointer;
}

.left-menu li a {
color: #333;
text-decoration: none;
display: flex;
align-items: center;
}

li a i {
margin-right: 10px;
}

li.active {
background-color: #ccc;
}

li.active a {
color: #fff;
}
</style>